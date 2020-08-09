namespace Babel
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.lblTargetLocale = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.ofdKeyFile = new System.Windows.Forms.OpenFileDialog();
            this.cmbLocale = new System.Windows.Forms.ComboBox();
            this.cbxDummy = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxAutoOCR = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbxDisplayTimes = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtKeyFile = new System.Windows.Forms.TextBox();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.btnBrowseKeyFile = new System.Windows.Forms.Button();
            this.lblKeyFile = new System.Windows.Forms.Label();
            this.lblProjectName = new System.Windows.Forms.Label();
            this.lblMaxReq = new System.Windows.Forms.Label();
            this.numRateLimit = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRateLimit)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTargetLocale
            // 
            this.lblTargetLocale.AutoSize = true;
            this.lblTargetLocale.Location = new System.Drawing.Point(6, 16);
            this.lblTargetLocale.Name = "lblTargetLocale";
            this.lblTargetLocale.Size = new System.Drawing.Size(76, 13);
            this.lblTargetLocale.TabIndex = 3;
            this.lblTargetLocale.Text = "Target Locale:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(318, 257);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(399, 257);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ofdKeyFile
            // 
            this.ofdKeyFile.DefaultExt = "json";
            // 
            // cmbLocale
            // 
            this.cmbLocale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocale.FormattingEnabled = true;
            this.cmbLocale.Items.AddRange(new object[] {
            "en",
            "es"});
            this.cmbLocale.Location = new System.Drawing.Point(88, 13);
            this.cmbLocale.Name = "cmbLocale";
            this.cmbLocale.Size = new System.Drawing.Size(121, 21);
            this.cmbLocale.TabIndex = 10;
            this.cmbLocale.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // cbxDummy
            // 
            this.cbxDummy.AutoSize = true;
            this.cbxDummy.Location = new System.Drawing.Point(11, 19);
            this.cbxDummy.Name = "cbxDummy";
            this.cbxDummy.Size = new System.Drawing.Size(260, 17);
            this.cbxDummy.TabIndex = 11;
            this.cbxDummy.Text = "Use dummy data (disables calls to Google API - $)";
            this.cbxDummy.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxAutoOCR);
            this.groupBox1.Controls.Add(this.lblTargetLocale);
            this.groupBox1.Controls.Add(this.cmbLocale);
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(477, 63);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // cbxAutoOCR
            // 
            this.cbxAutoOCR.AutoSize = true;
            this.cbxAutoOCR.Location = new System.Drawing.Point(6, 40);
            this.cbxAutoOCR.Name = "cbxAutoOCR";
            this.cbxAutoOCR.Size = new System.Drawing.Size(220, 17);
            this.cbxAutoOCR.TabIndex = 11;
            this.cbxAutoOCR.Text = "Automatically identify text on snapshot ($)";
            this.cbxAutoOCR.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbxDisplayTimes);
            this.groupBox2.Controls.Add(this.cbxDummy);
            this.groupBox2.Location = new System.Drawing.Point(5, 178);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(477, 73);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Debug";
            // 
            // cbxDisplayTimes
            // 
            this.cbxDisplayTimes.AutoSize = true;
            this.cbxDisplayTimes.Location = new System.Drawing.Point(11, 42);
            this.cbxDisplayTimes.Name = "cbxDisplayTimes";
            this.cbxDisplayTimes.Size = new System.Drawing.Size(185, 17);
            this.cbxDisplayTimes.TabIndex = 11;
            this.cbxDisplayTimes.Text = "Display translation round-trip times";
            this.cbxDisplayTimes.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.numRateLimit);
            this.groupBox3.Controls.Add(this.lblMaxReq);
            this.groupBox3.Controls.Add(this.txtKeyFile);
            this.groupBox3.Controls.Add(this.txtProjectName);
            this.groupBox3.Controls.Add(this.btnBrowseKeyFile);
            this.groupBox3.Controls.Add(this.lblKeyFile);
            this.groupBox3.Controls.Add(this.lblProjectName);
            this.groupBox3.Location = new System.Drawing.Point(8, 74);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(474, 98);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Backend Server Settings";
            // 
            // txtKeyFile
            // 
            this.txtKeyFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKeyFile.Location = new System.Drawing.Point(85, 18);
            this.txtKeyFile.Name = "txtKeyFile";
            this.txtKeyFile.Size = new System.Drawing.Size(300, 20);
            this.txtKeyFile.TabIndex = 10;
            // 
            // txtProjectName
            // 
            this.txtProjectName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProjectName.Location = new System.Drawing.Point(86, 45);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Size = new System.Drawing.Size(380, 20);
            this.txtProjectName.TabIndex = 11;
            // 
            // btnBrowseKeyFile
            // 
            this.btnBrowseKeyFile.Location = new System.Drawing.Point(391, 16);
            this.btnBrowseKeyFile.Name = "btnBrowseKeyFile";
            this.btnBrowseKeyFile.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseKeyFile.TabIndex = 14;
            this.btnBrowseKeyFile.Text = "Browse...";
            this.btnBrowseKeyFile.UseVisualStyleBackColor = true;
            // 
            // lblKeyFile
            // 
            this.lblKeyFile.AutoSize = true;
            this.lblKeyFile.Location = new System.Drawing.Point(33, 21);
            this.lblKeyFile.Name = "lblKeyFile";
            this.lblKeyFile.Size = new System.Drawing.Size(47, 13);
            this.lblKeyFile.TabIndex = 12;
            this.lblKeyFile.Text = "Key File:";
            // 
            // lblProjectName
            // 
            this.lblProjectName.AutoSize = true;
            this.lblProjectName.Location = new System.Drawing.Point(6, 48);
            this.lblProjectName.Name = "lblProjectName";
            this.lblProjectName.Size = new System.Drawing.Size(74, 13);
            this.lblProjectName.TabIndex = 13;
            this.lblProjectName.Text = "Project Name:";
            // 
            // lblMaxReq
            // 
            this.lblMaxReq.AutoSize = true;
            this.lblMaxReq.Location = new System.Drawing.Point(22, 74);
            this.lblMaxReq.Name = "lblMaxReq";
            this.lblMaxReq.Size = new System.Drawing.Size(57, 13);
            this.lblMaxReq.TabIndex = 15;
            this.lblMaxReq.Text = "Rate Limit:";
            // 
            // numRateLimit
            // 
            this.numRateLimit.Location = new System.Drawing.Point(86, 72);
            this.numRateLimit.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numRateLimit.Name = "numRateLimit";
            this.numRateLimit.Size = new System.Drawing.Size(120, 20);
            this.numRateLimit.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(212, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "(requests per second)";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 288);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Settings";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.VisibleChanged += new System.EventHandler(this.Settings_VisibleChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRateLimit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblTargetLocale;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.OpenFileDialog ofdKeyFile;
        private System.Windows.Forms.ComboBox cmbLocale;
        private System.Windows.Forms.CheckBox cbxDummy;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbxDisplayTimes;
        private System.Windows.Forms.CheckBox cbxAutoOCR;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numRateLimit;
        private System.Windows.Forms.Label lblMaxReq;
        private System.Windows.Forms.TextBox txtKeyFile;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.Button btnBrowseKeyFile;
        private System.Windows.Forms.Label lblKeyFile;
        private System.Windows.Forms.Label lblProjectName;
    }
}