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
      } catch (Exception ex) {
        MessageBox.Show(this, ex.Message + Environment.NewLine + ex.StackTrace, this.Text,
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    public void ProcessPath(DirectoryInfo src) {
      DirectoryInfo trg = new DirectoryInfo(src + @"\Processed\");
      if (!trg.Exists)
        trg.Create();

      FileInfo[] files = src.GetFiles("*.kmz");
      lblProcessing.Text = string.Format("Processing {0} files in '{1}'.", files.Length, src);
      foreach (FileInfo fi in files) {
        ProcessFile(fi, trg);
      }
    }

    public void ProcessFile(FileInfo fi, DirectoryInfo trg) {
      using (FileStream fs = fi.OpenRead()) {
        using (ZipFile zip = new ZipFile(fs)) {
          for (int i = 0; i < zip.Count; i++) {
            ZipEntry entry = zip[i];
            if (entry.Name.EndsWith(".kml")) {
              ProcessEntry(zip, entry, trg);
            }
          }
        }
      }
    }

    public void ProcessEntry(ZipFile zip, ZipEntry entry, DirectoryInfo dir) {
      FileInfo trgFile = new FileInfo(dir.FullName + "\\" + entry.Name);

      //byte[] buff = new byte[4096];
      string kmlText;
      using (Stream src = zip.GetInputStream(entry)) {
        /*			using (FileStream trg = new FileStream(trgFile.FullName, FileMode.Create, FileAccess.Write)) {
                        int read;
                        do {
                            read = src.Read(buff, 0, buff.Length);
                            trg.Write(buff, 0, read);
                        } while (read > 0);
                    }*/
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
    }
  }
}
