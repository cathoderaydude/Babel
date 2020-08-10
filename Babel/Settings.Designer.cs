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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRefreshGSL = new System.Windows.Forms.Button();
            this.cbxAutoOCR = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbxDisplayTimes = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numRateLimit = new System.Windows.Forms.NumericUpDown();
            this.lblMaxReq = new System.Windows.Forms.Label();
            this.txtGoogleKeyFile = new System.Windows.Forms.TextBox();
            this.txtGoogleProjectName = new System.Windows.Forms.TextBox();
            this.btnBrowseKeyFile = new System.Windows.Forms.Button();
            this.lblKeyFile = new System.Windows.Forms.Label();
            this.lblProjectName = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtMsOcrEndpoint = new System.Windows.Forms.TextBox();
            this.txtMsOcrApiKey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rbMicrosoft = new System.Windows.Forms.RadioButton();
            this.rbGoogle = new System.Windows.Forms.RadioButton();
            this.rbDummy = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMsTranslatorApiKey = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRateLimit)).BeginInit();
            this.groupBox4.SuspendLayout();
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
            this.btnOk.Location = new System.Drawing.Point(306, 458);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(399, 458);
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
            this.cmbLocale.Location = new System.Drawing.Point(88, 13);
            this.cmbLocale.Name = "cmbLocale";
            this.cmbLocale.Size = new System.Drawing.Size(219, 21);
            this.cmbLocale.TabIndex = 10;
            this.cmbLocale.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRefreshGSL);
            this.groupBox1.Controls.Add(this.cbxAutoOCR);
            this.groupBox1.Controls.Add(this.lblTargetLocale);
            this.groupBox1.Controls.Add(this.cmbLocale);
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(477, 63);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // btnRefreshGSL
            // 
            this.btnRefreshGSL.Location = new System.Drawing.Point(313, 11);
            this.btnRefreshGSL.Name = "btnRefreshGSL";
            this.btnRefreshGSL.Size = new System.Drawing.Size(156, 23);
            this.btnRefreshGSL.TabIndex = 12;
            this.btnRefreshGSL.Text = "Refresh Supported Locales";
            this.btnRefreshGSL.UseVisualStyleBackColor = true;
            this.btnRefreshGSL.Click += new System.EventHandler(this.btnRefreshGSL_Click);
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
            this.groupBox3.Controls.Add(this.txtGoogleKeyFile);
            this.groupBox3.Controls.Add(this.txtGoogleProjectName);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(212, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "(requests per second)";
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
            // lblMaxReq
            // 
            this.lblMaxReq.AutoSize = true;
            this.lblMaxReq.Location = new System.Drawing.Point(22, 74);
            this.lblMaxReq.Name = "lblMaxReq";
            this.lblMaxReq.Size = new System.Drawing.Size(57, 13);
            this.lblMaxReq.TabIndex = 15;
            this.lblMaxReq.Text = "Rate Limit:";
            // 
            // txtGoogleKeyFile
            // 
            this.txtGoogleKeyFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGoogleKeyFile.Location = new System.Drawing.Point(85, 18);
            this.txtGoogleKeyFile.Name = "txtGoogleKeyFile";
            this.txtGoogleKeyFile.Size = new System.Drawing.Size(300, 20);
            this.txtGoogleKeyFile.TabIndex = 10;
            // 
            // txtGoogleProjectName
            // 
            this.txtGoogleProjectName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGoogleProjectName.Location = new System.Drawing.Point(86, 45);
            this.txtGoogleProjectName.Name = "txtGoogleProjectName";
            this.txtGoogleProjectName.Size = new System.Drawing.Size(380, 20);
            this.txtGoogleProjectName.TabIndex = 11;
            // 
            // btnBrowseKeyFile
            // 
            this.btnBrowseKeyFile.Location = new System.Drawing.Point(391, 16);
            this.btnBrowseKeyFile.Name = "btnBrowseKeyFile";
            this.btnBrowseKeyFile.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseKeyFile.TabIndex = 14;
            this.btnBrowseKeyFile.Text = "Browse...";
            this.btnBrowseKeyFile.UseVisualStyleBackColor = true;
            this.btnBrowseKeyFile.Click += new System.EventHandler(this.btnBrowseKeyFile_Click);
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
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtMsTranslatorApiKey);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.txtMsOcrEndpoint);
            this.groupBox4.Controls.Add(this.txtMsOcrApiKey);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.rbMicrosoft);
            this.groupBox4.Controls.Add(this.rbGoogle);
            this.groupBox4.Controls.Add(this.rbDummy);
            this.groupBox4.Location = new System.Drawing.Point(8, 257);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(474, 195);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "groupBox4";
            // 
            // txtMsOcrEndpoint
            // 
            this.txtMsOcrEndpoint.Location = new System.Drawing.Point(124, 115);
            this.txtMsOcrEndpoint.Name = "txtMsOcrEndpoint";
            this.txtMsOcrEndpoint.Size = new System.Drawing.Size(342, 20);
            this.txtMsOcrEndpoint.TabIndex = 6;
            // 
            // txtMsOcrApiKey
            // 
            this.txtMsOcrApiKey.Location = new System.Drawing.Point(124, 91);
            this.txtMsOcrApiKey.Name = "txtMsOcrApiKey";
            this.txtMsOcrApiKey.PasswordChar = '*';
            this.txtMsOcrApiKey.Size = new System.Drawing.Size(342, 20);
            this.txtMsOcrApiKey.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "MS API Endpoint";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "MS OCR API Key";
            // 
            // rbMicrosoft
            // 
            this.rbMicrosoft.AutoSize = true;
            this.rbMicrosoft.Location = new System.Drawing.Point(6, 65);
            this.rbMicrosoft.Name = "rbMicrosoft";
            this.rbMicrosoft.Size = new System.Drawing.Size(68, 17);
            this.rbMicrosoft.TabIndex = 2;
            this.rbMicrosoft.TabStop = true;
            this.rbMicrosoft.Text = "Microsoft";
            this.rbMicrosoft.UseVisualStyleBackColor = true;
            // 
            // rbGoogle
            // 
            this.rbGoogle.AutoSize = true;
            this.rbGoogle.Location = new System.Drawing.Point(6, 42);
            this.rbGoogle.Name = "rbGoogle";
            this.rbGoogle.Size = new System.Drawing.Size(59, 17);
            this.rbGoogle.TabIndex = 1;
            this.rbGoogle.TabStop = true;
            this.rbGoogle.Text = "Google";
            this.rbGoogle.UseVisualStyleBackColor = true;
            // 
            // rbDummy
            // 
            this.rbDummy.AutoSize = true;
            this.rbDummy.Location = new System.Drawing.Point(6, 19);
            this.rbDummy.Name = "rbDummy";
            this.rbDummy.Size = new System.Drawing.Size(60, 17);
            this.rbDummy.TabIndex = 0;
            this.rbDummy.TabStop = true;
            this.rbDummy.Text = "Dummy";
            this.rbDummy.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "MS Translator API Key";
            // 
            // txtMsTranslatorApiKey
            // 
            this.txtMsTranslatorApiKey.Location = new System.Drawing.Point(124, 141);
            this.txtMsTranslatorApiKey.Name = "txtMsTranslatorApiKey";
            this.txtMsTranslatorApiKey.PasswordChar = '*';
            this.txtMsTranslatorApiKey.Size = new System.Drawing.Size(342, 20);
            this.txtMsTranslatorApiKey.TabIndex = 8;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 489);
            this.Controls.Add(this.groupBox4);
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
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblTargetLocale;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.OpenFileDialog ofdKeyFile;
        private System.Windows.Forms.ComboBox cmbLocale;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbxDisplayTimes;
        private System.Windows.Forms.CheckBox cbxAutoOCR;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numRateLimit;
        private System.Windows.Forms.Label lblMaxReq;
        private System.Windows.Forms.TextBox txtGoogleKeyFile;
        private System.Windows.Forms.TextBox txtGoogleProjectName;
        private System.Windows.Forms.Button btnBrowseKeyFile;
        private System.Windows.Forms.Label lblKeyFile;
        private System.Windows.Forms.Label lblProjectName;
        private System.Windows.Forms.Button btnRefreshGSL;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbMicrosoft;
        private System.Windows.Forms.RadioButton rbGoogle;
        private System.Windows.Forms.RadioButton rbDummy;
        private System.Windows.Forms.TextBox txtMsOcrEndpoint;
        private System.Windows.Forms.TextBox txtMsOcrApiKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMsTranslatorApiKey;
        private System.Windows.Forms.Label label4;
    }
}