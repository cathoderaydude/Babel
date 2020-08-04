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
            this.SuspendLayout();
            // 
            // txtKeyFile
            // 
            this.txtKeyFile.Location = new System.Drawing.Point(94, 38);
            this.txtKeyFile.Name = "txtKeyFile";
            this.txtKeyFile.Size = new System.Drawing.Size(297, 20);
            this.txtKeyFile.TabIndex = 1;
            this.txtKeyFile.TextChanged += new System.EventHandler(this.txtKeyFile_TextChanged);
            // 
            // txtProjectName
            // 
            this.txtProjectName.Location = new System.Drawing.Point(94, 64);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Size = new System.Drawing.Size(378, 20);
            this.txtProjectName.TabIndex = 2;
            this.txtProjectName.TextChanged += new System.EventHandler(this.txtProjectName_TextChanged);
            // 
            // lblTargetLocale
            // 
            this.lblTargetLocale.AutoSize = true;
            this.lblTargetLocale.Location = new System.Drawing.Point(12, 15);
            this.lblTargetLocale.Name = "lblTargetLocale";
            this.lblTargetLocale.Size = new System.Drawing.Size(76, 13);
            this.lblTargetLocale.TabIndex = 3;
            this.lblTargetLocale.Text = "Target Locale:";
            // 
            // lblKeyFile
            // 
            this.lblKeyFile.AutoSize = true;
            this.lblKeyFile.Location = new System.Drawing.Point(41, 41);
            this.lblKeyFile.Name = "lblKeyFile";
            this.lblKeyFile.Size = new System.Drawing.Size(47, 13);
            this.lblKeyFile.TabIndex = 4;
            this.lblKeyFile.Text = "Key File:";
            // 
            // lblProjectName
            // 
            this.lblProjectName.AutoSize = true;
            this.lblProjectName.Location = new System.Drawing.Point(14, 67);
            this.lblProjectName.Name = "lblProjectName";
            this.lblProjectName.Size = new System.Drawing.Size(74, 13);
            this.lblProjectName.TabIndex = 5;
            this.lblProjectName.Text = "Project Name:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(316, 90);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(397, 90);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnBrowseKeyFile
            // 
            this.btnBrowseKeyFile.Location = new System.Drawing.Point(397, 35);
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
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 125);
            this.Controls.Add(this.btnBrowseKeyFile);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblProjectName);
            this.Controls.Add(this.lblKeyFile);
            this.Controls.Add(this.lblTargetLocale);
            this.Controls.Add(this.txtProjectName);
            this.Controls.Add(this.txtKeyFile);
            this.Name = "Settings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}