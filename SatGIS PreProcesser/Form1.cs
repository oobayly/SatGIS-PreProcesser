using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using ICSharpCode.SharpZipLib.Zip;

namespace SatGIS_PreProcesser {
  public partial class Form1 : Form {
    public Form1() {
      InitializeComponent();
    }

    private void butProcess_Click(object sender, EventArgs e) {
      DirectoryInfo dir = null;
      if (!string.IsNullOrEmpty(Properties.Settings.Default.LastPath)) {
        dir = new DirectoryInfo(Properties.Settings.Default.LastPath);
      }
      if (dir == null || !dir.Exists)
        dir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

      folderSelector.SelectedPath = dir.FullName;
      if (folderSelector.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
        return;

      Properties.Settings.Default.LastPath = folderSelector.SelectedPath;
      Properties.Settings.Default.Save();

      try {
        ProcessPath(new DirectoryInfo(folderSelector.SelectedPath));
#if DEBUG
      } finally { }
#else
      } catch (Exception ex) {
        MessageBox.Show(this, ex.Message + Environment.NewLine + ex.StackTrace, this.Text,
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
#endif
    }

    private void MergeFiles(DirectoryInfo src, IEnumerable<FileInfo> files) {
      XmlDocument merged = new XmlDocument();
      XmlElement kml = merged.CreateElement("", "kml", "http://earth.google.com/kml/2.1");
      merged.AppendChild(kml);
      XmlElement document = merged.CreateElement("", "Document", "");
      merged.DocumentElement.AppendChild(document);

      foreach (FileInfo fi in files) {
        XmlDocument doc = new XmlDocument();
        doc.Load(fi.FullName);

        XmlNamespaceManager mgr = new XmlNamespaceManager(doc.NameTable);
        mgr.AddNamespace("x", doc.DocumentElement.NamespaceURI);

        // Line
        XmlNode line = doc.SelectSingleNode(@"//x:Placemark/x:LineString", mgr);
        if (line != null) {
          document.AppendChild(merged.ImportNode(line.ParentNode, true));
        }
      }

      merged.Save(Path.Combine(src.FullName, "All.kml"));
    }

    public void ProcessPath(DirectoryInfo src) {
      DirectoryInfo trg = new DirectoryInfo(Path.Combine(src.FullName, "Processed"));
      if (!trg.Exists)
        trg.Create();

      FileInfo[] files = src.GetFiles("*.kmz");
      lblProcessing.Text = string.Format("Processing {0} files in '{1}'.", files.Length, src);
      List<FileInfo> processed = new List<FileInfo>();
      foreach (FileInfo fi in files) {
        FileInfo output = ProcessFile(fi, trg);
        if (output != null)
          processed.Add(output);
      }

      MergeFiles(trg, processed);
    }

    public FileInfo ProcessFile(FileInfo kmzFile, DirectoryInfo trg) {
      using (FileStream fs = kmzFile.OpenRead()) {
        using (ZipFile zip = new ZipFile(fs)) {
          for (int i = 0; i < zip.Count; i++) {
            ZipEntry entry = zip[i];
            if (entry.Name.EndsWith(".kml")) {
              return ProcessEntry(kmzFile, zip, entry, trg);
            }
          }
        }
      }

      return null;
    }

    public FileInfo ProcessEntry(FileInfo kmzFile, ZipFile zip, ZipEntry entry, DirectoryInfo dir) {
      string trgName = kmzFile.Name.Replace(kmzFile.Extension, ".kml");
      FileInfo trgFile = new FileInfo(Path.Combine(dir.FullName, trgName));

      string kmlText;
      using (Stream src = zip.GetInputStream(entry)) {
        using (StreamReader reader = new StreamReader(src)) {
          kmlText = reader.ReadToEnd();
        }
      }

      /* avmap namespace is incorrectly references as xmlns:prefix="avmap"
       * it should look like: xmlns:avmap="avmap", so do a simple replace
       */
      kmlText = kmlText.Replace("xmlns:prefix=\"avmap\"", "xmlns:avmap=\"avmap\"");

      // Make sure the document can be opened
      XmlDocument doc = new XmlDocument();
      doc.LoadXml(kmlText);

      // And save
      doc.Save(trgFile.FullName);
      listProcessed.Items.Add(string.Format("Processed {0}", trgFile.Name));

      return trgFile;
    }
  }
}
