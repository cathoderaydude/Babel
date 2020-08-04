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
            this.txtKeyFile = new System.Windows.Forms.TextBox();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.lblTargetLocale = new System.Windows.Forms.Label();
            this.lblKeyFile = new System.Windows.Forms.Label();
            this.lblProjectName = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnBrowseKeyFile = new System.Windows.Forms.Button();
            this.ofdKeyFile = new System.Windows.Forms.OpenFileDialog();
            this.cmbLocale = new System.Windows.Forms.ComboBox();
            this.cbxDummy = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtKeyFile
            // 
            this.txtKeyFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKeyFile.Location = new System.Drawing.Point(88, 39);
            this.txtKeyFile.Name = "txtKeyFile";
            this.txtKeyFile.Size = new System.Drawing.Size(297, 20);
            this.txtKeyFile.TabIndex = 1;
            this.txtKeyFile.TextChanged += new System.EventHandler(this.txtKeyFile_TextChanged);
            // 
            // txtProjectName
            // 
            this.txtProjectName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProjectName.Location = new System.Drawing.Point(88, 65);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Size = new System.Drawing.Size(378, 20);
            this.txtProjectName.TabIndex = 2;
            this.txtProjectName.TextChanged += new System.EventHandler(this.txtProjectName_TextChanged);
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
            // lblKeyFile
            // 
            this.lblKeyFile.AutoSize = true;
            this.lblKeyFile.Location = new System.Drawing.Point(35, 42);
            this.lblKeyFile.Name = "lblKeyFile";
            this.lblKeyFile.Size = new System.Drawing.Size(47, 13);
            this.lblKeyFile.TabIndex = 4;
            this.lblKeyFile.Text = "Key File:";
            // 
            // lblProjectName
            // 
            this.lblProjectName.AutoSize = true;
            this.lblProjectName.Location = new System.Drawing.Point(8, 68);
            this.lblProjectName.Name = "lblProjectName";
            this.lblProjectName.Size = new System.Drawing.Size(74, 13);
            this.lblProjectName.TabIndex = 5;
            this.lblProjectName.Text = "Project Name:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(310, 91);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(391, 91);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnBrowseKeyFile
            // 
            this.btnBrowseKeyFile.Location = new System.Drawing.Point(391, 36);
            this.btnBrowseKeyFile.Name = "btnBrowseKeyFile";
            this.btnBrowseKeyFile.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseKeyFile.TabIndex = 9;
            this.btnBrowseKeyFile.Text = "Browse...";
            this.btnBrowseKeyFile.UseVisualStyleBackColor = true;
            this.btnBrowseKeyFile.Click += new System.EventHandler(this.btnBrowseKeyFile_Click);
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
            this.cbxDummy.Location = new System.Drawing.Point(377, 12);
            this.cbxDummy.Name = "cbxDummy";
            this.cbxDummy.Size = new System.Drawing.Size(85, 17);
            this.cbxDummy.TabIndex = 11;
            this.cbxDummy.Text = "Dummy data";
            this.cbxDummy.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTargetLocale);
            this.groupBox1.Controls.Add(this.cbxDummy);
            this.groupBox1.Controls.Add(this.txtKeyFile);
            this.groupBox1.Controls.Add(this.cmbLocale);
            this.groupBox1.Controls.Add(this.txtProjectName);
            this.groupBox1.Controls.Add(this.btnBrowseKeyFile);
            this.groupBox1.Controls.Add(this.lblKeyFile);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.lblProjectName);
            this.groupBox1.Controls.Add(this.btnOk);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(477, 126);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 136);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Settings";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtKeyFile;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.Label lblTargetLocale;
        private System.Windows.Forms.Label lblKeyFile;
        private System.Windows.Forms.Label lblProjectName;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnBrowseKeyFile;
        private System.Windows.Forms.OpenFileDialog ofdKeyFile;
        private System.Windows.Forms.ComboBox cmbLocale;
        private System.Windows.Forms.CheckBox cbxDummy;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}