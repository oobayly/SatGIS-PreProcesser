namespace SatGIS_PreProcesser
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.butProcess = new System.Windows.Forms.Button();
            this.folderSelector = new System.Windows.Forms.FolderBrowserDialog();
            this.lblProcessing = new System.Windows.Forms.Label();
            this.listProcessed = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // butProcess
            // 
            this.butProcess.Location = new System.Drawing.Point(3, 3);
            this.butProcess.Name = "butProcess";
            this.butProcess.Size = new System.Drawing.Size(75, 23);
            this.butProcess.TabIndex = 0;
            this.butProcess.Text = "Process Directory";
            this.butProcess.UseVisualStyleBackColor = true;
            this.butProcess.Click += new System.EventHandler(this.butProcess_Click);
            // 
            // folderSelector
            // 
            this.folderSelector.Description = "Select the folder containing the KMZ files to process";
            this.folderSelector.ShowNewFolderButton = false;
            // 
            // lblProcessing
            // 
            this.lblProcessing.AutoSize = true;
            this.lblProcessing.Location = new System.Drawing.Point(3, 29);
            this.lblProcessing.Name = "lblProcessing";
            this.lblProcessing.Size = new System.Drawing.Size(16, 13);
            this.lblProcessing.TabIndex = 1;
            this.lblProcessing.Text = "...";
            // 
            // listProcessed
            // 
            this.listProcessed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listProcessed.FormattingEnabled = true;
            this.listProcessed.IntegralHeight = false;
            this.listProcessed.Location = new System.Drawing.Point(3, 45);
            this.listProcessed.Name = "listProcessed";
            this.listProcessed.Size = new System.Drawing.Size(291, 292);
            this.listProcessed.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.butProcess, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.listProcessed, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblProcessing, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(297, 315);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 315);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "SatGIS PreProcessor";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button butProcess;
        private System.Windows.Forms.FolderBrowserDialog folderSelector;
        private System.Windows.Forms.Label lblProcessing;
        private System.Windows.Forms.ListBox listProcessed;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}

