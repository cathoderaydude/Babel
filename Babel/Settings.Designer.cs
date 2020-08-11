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
            this.btnRefreshGSL = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxDisplayTimes = new System.Windows.Forms.CheckBox();
            this.numRateLimit = new System.Windows.Forms.NumericUpDown();
            this.lblMaxReq = new System.Windows.Forms.Label();
            this.txtGoogleKeyFile = new System.Windows.Forms.TextBox();
            this.txtGoogleProjectName = new System.Windows.Forms.TextBox();
            this.btnBrowseKeyFile = new System.Windows.Forms.Button();
            this.lblKeyFile = new System.Windows.Forms.Label();
            this.lblProjectName = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cboTranslation = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cboOCR = new System.Windows.Forms.ComboBox();
            this.txtMsTranslatorApiKey = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMsOcrEndpoint = new System.Windows.Forms.TextBox();
            this.txtMsOcrApiKey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDeepLKey = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRateLimit)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTargetLocale
            // 
            this.lblTargetLocale.AutoSize = true;
            this.lblTargetLocale.Location = new System.Drawing.Point(6, 79);
            this.lblTargetLocale.Name = "lblTargetLocale";
            this.lblTargetLocale.Size = new System.Drawing.Size(76, 13);
            this.lblTargetLocale.TabIndex = 3;
            this.lblTargetLocale.Text = "Target Locale:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(288, 384);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(381, 384);
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
            this.cmbLocale.DisplayMember = "source";
            this.cmbLocale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocale.FormattingEnabled = true;
            this.cmbLocale.Location = new System.Drawing.Point(88, 76);
            this.cmbLocale.Name = "cmbLocale";
            this.cmbLocale.Size = new System.Drawing.Size(178, 21);
            this.cmbLocale.TabIndex = 10;
            this.cmbLocale.ValueMember = "code";
            this.cmbLocale.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btnRefreshGSL
            // 
            this.btnRefreshGSL.Location = new System.Drawing.Point(270, 74);
            this.btnRefreshGSL.Name = "btnRefreshGSL";
            this.btnRefreshGSL.Size = new System.Drawing.Size(156, 23);
            this.btnRefreshGSL.TabIndex = 12;
            this.btnRefreshGSL.Text = "Refresh Supported Locales";
            this.btnRefreshGSL.UseVisualStyleBackColor = true;
            this.btnRefreshGSL.Click += new System.EventHandler(this.btnRefreshGSL_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cbxDisplayTimes);
            this.groupBox2.Controls.Add(this.numRateLimit);
            this.groupBox2.Controls.Add(this.lblMaxReq);
            this.groupBox2.Location = new System.Drawing.Point(8, 305);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(448, 73);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Misc";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(201, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "(requests per second)";
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
            // numRateLimit
            // 
            this.numRateLimit.Location = new System.Drawing.Point(75, 14);
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
            this.lblMaxReq.Location = new System.Drawing.Point(11, 16);
            this.lblMaxReq.Name = "lblMaxReq";
            this.lblMaxReq.Size = new System.Drawing.Size(57, 13);
            this.lblMaxReq.TabIndex = 15;
            this.lblMaxReq.Text = "Rate Limit:";
            // 
            // txtGoogleKeyFile
            // 
            this.txtGoogleKeyFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGoogleKeyFile.Location = new System.Drawing.Point(88, 23);
            this.txtGoogleKeyFile.Name = "txtGoogleKeyFile";
            this.txtGoogleKeyFile.Size = new System.Drawing.Size(254, 20);
            this.txtGoogleKeyFile.TabIndex = 10;
            // 
            // txtGoogleProjectName
            // 
            this.txtGoogleProjectName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGoogleProjectName.Location = new System.Drawing.Point(88, 50);
            this.txtGoogleProjectName.Name = "txtGoogleProjectName";
            this.txtGoogleProjectName.Size = new System.Drawing.Size(338, 20);
            this.txtGoogleProjectName.TabIndex = 11;
            // 
            // btnBrowseKeyFile
            // 
            this.btnBrowseKeyFile.Location = new System.Drawing.Point(351, 21);
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
            this.lblKeyFile.Location = new System.Drawing.Point(36, 26);
            this.lblKeyFile.Name = "lblKeyFile";
            this.lblKeyFile.Size = new System.Drawing.Size(47, 13);
            this.lblKeyFile.TabIndex = 12;
            this.lblKeyFile.Text = "Key File:";
            // 
            // lblProjectName
            // 
            this.lblProjectName.AutoSize = true;
            this.lblProjectName.Location = new System.Drawing.Point(9, 53);
            this.lblProjectName.Name = "lblProjectName";
            this.lblProjectName.Size = new System.Drawing.Size(74, 13);
            this.lblProjectName.TabIndex = 13;
            this.lblProjectName.Text = "Project Name:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cboTranslation);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.cboOCR);
            this.groupBox4.Location = new System.Drawing.Point(8, 8);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(448, 94);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Backend Services";
            // 
            // cboTranslation
            // 
            this.cboTranslation.DisplayMember = "name";
            this.cboTranslation.FormattingEnabled = true;
            this.cboTranslation.Location = new System.Drawing.Point(118, 48);
            this.cboTranslation.Name = "cboTranslation";
            this.cboTranslation.Size = new System.Drawing.Size(182, 21);
            this.cboTranslation.TabIndex = 7;
            this.cboTranslation.ValueMember = "source";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Translation Service:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(40, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "OCR Service:";
            // 
            // cboOCR
            // 
            this.cboOCR.DisplayMember = "name";
            this.cboOCR.FormattingEnabled = true;
            this.cboOCR.Location = new System.Drawing.Point(118, 21);
            this.cboOCR.Name = "cboOCR";
            this.cboOCR.Size = new System.Drawing.Size(182, 21);
            this.cboOCR.TabIndex = 4;
            this.cboOCR.ValueMember = "source";
            // 
            // txtMsTranslatorApiKey
            // 
            this.txtMsTranslatorApiKey.Location = new System.Drawing.Point(124, 18);
            this.txtMsTranslatorApiKey.Name = "txtMsTranslatorApiKey";
            this.txtMsTranslatorApiKey.PasswordChar = '*';
            this.txtMsTranslatorApiKey.Size = new System.Drawing.Size(299, 20);
            this.txtMsTranslatorApiKey.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "MS Translator API Key";
            // 
            // txtMsOcrEndpoint
            // 
            this.txtMsOcrEndpoint.Location = new System.Drawing.Point(124, 42);
            this.txtMsOcrEndpoint.Name = "txtMsOcrEndpoint";
            this.txtMsOcrEndpoint.Size = new System.Drawing.Size(299, 20);
            this.txtMsOcrEndpoint.TabIndex = 6;
            // 
            // txtMsOcrApiKey
            // 
            this.txtMsOcrApiKey.Location = new System.Drawing.Point(124, 18);
            this.txtMsOcrApiKey.Name = "txtMsOcrApiKey";
            this.txtMsOcrApiKey.PasswordChar = '*';
            this.txtMsOcrApiKey.Size = new System.Drawing.Size(299, 20);
            this.txtMsOcrApiKey.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "MS API Endpoint";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "MS OCR API Key";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(8, 108);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(452, 191);
            this.tabControl1.TabIndex = 16;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox7);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(444, 165);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Google";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.lblTargetLocale);
            this.groupBox7.Controls.Add(this.btnRefreshGSL);
            this.groupBox7.Controls.Add(this.txtGoogleProjectName);
            this.groupBox7.Controls.Add(this.lblProjectName);
            this.groupBox7.Controls.Add(this.txtGoogleKeyFile);
            this.groupBox7.Controls.Add(this.lblKeyFile);
            this.groupBox7.Controls.Add(this.btnBrowseKeyFile);
            this.groupBox7.Controls.Add(this.cmbLocale);
            this.groupBox7.Location = new System.Drawing.Point(6, 15);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(432, 144);
            this.groupBox7.TabIndex = 15;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "OCR && Translation";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox6);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(444, 165);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Microsoft";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label4);
            this.groupBox6.Controls.Add(this.txtMsTranslatorApiKey);
            this.groupBox6.Location = new System.Drawing.Point(9, 96);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(429, 59);
            this.groupBox6.TabIndex = 10;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Translation";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.txtMsOcrApiKey);
            this.groupBox5.Controls.Add(this.txtMsOcrEndpoint);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Location = new System.Drawing.Point(9, 17);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(429, 73);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "OCR";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox8);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(5);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(444, 165);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "DeepL";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label5);
            this.groupBox8.Controls.Add(this.txtDeepLKey);
            this.groupBox8.Location = new System.Drawing.Point(12, 9);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(418, 142);
            this.groupBox8.TabIndex = 0;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Translation";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "API Key:";
            // 
            // txtDeepLKey
            // 
            this.txtDeepLKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDeepLKey.Location = new System.Drawing.Point(68, 23);
            this.txtDeepLKey.Name = "txtDeepLKey";
            this.txtDeepLKey.Size = new System.Drawing.Size(344, 20);
            this.txtDeepLKey.TabIndex = 11;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 421);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
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
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRateLimit)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblTargetLocale;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.OpenFileDialog ofdKeyFile;
        private System.Windows.Forms.ComboBox cmbLocale;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbxDisplayTimes;
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
        private System.Windows.Forms.TextBox txtMsOcrEndpoint;
        private System.Windows.Forms.TextBox txtMsOcrApiKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMsTranslatorApiKey;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDeepLKey;
        private System.Windows.Forms.ComboBox cboTranslation;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboOCR;
    }
}