namespace Babel
{
    partial class frmBabel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBabel));
            this.pbxDisplay = new System.Windows.Forms.PictureBox();
            this.tscMain = new System.Windows.Forms.ToolStripContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbSnap = new System.Windows.Forms.ToolStripSplitButton();
            this.tsbClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbOCR = new System.Windows.Forms.ToolStripButton();
            this.tsbAutoOCR = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRevert = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripSplitButton();
            this.tsbSaveTranslated = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbSaveRaw = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbTextText = new System.Windows.Forms.ToolStripButton();
            this.tsbSettings = new System.Windows.Forms.ToolStripButton();
            this.sfdDisplay = new System.Windows.Forms.SaveFileDialog();
            this.txtPlaceholder = new System.Windows.Forms.TextBox();
            this.tsbVFW = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDisplay)).BeginInit();
            this.tscMain.ContentPanel.SuspendLayout();
            this.tscMain.TopToolStripPanel.SuspendLayout();
            this.tscMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbxDisplay
            // 
            this.pbxDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxDisplay.Location = new System.Drawing.Point(3, 3);
            this.pbxDisplay.MinimumSize = new System.Drawing.Size(100, 100);
            this.pbxDisplay.Name = "pbxDisplay";
            this.pbxDisplay.Size = new System.Drawing.Size(575, 371);
            this.pbxDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbxDisplay.TabIndex = 1;
            this.pbxDisplay.TabStop = false;
            this.pbxDisplay.Visible = false;
            this.pbxDisplay.Paint += new System.Windows.Forms.PaintEventHandler(this.pbxDisplay_Paint);
            this.pbxDisplay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbxDisplay_MouseDown);
            this.pbxDisplay.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbxDisplay_MouseMove);
            this.pbxDisplay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbxDisplay_MouseUp);
            // 
            // tscMain
            // 
            // 
            // tscMain.ContentPanel
            // 
            this.tscMain.ContentPanel.AutoScroll = true;
            this.tscMain.ContentPanel.Controls.Add(this.panel1);
            this.tscMain.ContentPanel.Size = new System.Drawing.Size(491, 297);
            this.tscMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tscMain.Location = new System.Drawing.Point(5, 5);
            this.tscMain.Name = "tscMain";
            this.tscMain.Size = new System.Drawing.Size(491, 328);
            this.tscMain.TabIndex = 7;
            this.tscMain.Text = "toolStripContainer1";
            // 
            // tscMain.TopToolStripPanel
            // 
            this.tscMain.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.txtPlaceholder);
            this.panel1.Controls.Add(this.pbxDisplay);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(491, 297);
            this.panel1.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSnap,
            this.tsbVFW,
            this.toolStripSeparator3,
            this.tsbOCR,
            this.tsbAutoOCR,
            this.toolStripSeparator1,
            this.tsbRevert,
            this.tsbSave,
            this.toolStripSeparator2,
            this.tsbTextText,
            this.tsbSettings});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(300, 31);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbSnap
            // 
            this.tsbSnap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSnap.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClipboard});
            this.tsbSnap.Image = ((System.Drawing.Image)(resources.GetObject("tsbSnap.Image")));
            this.tsbSnap.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbSnap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSnap.Name = "tsbSnap";
            this.tsbSnap.Size = new System.Drawing.Size(40, 28);
            this.tsbSnap.Text = "toolStripSplitButton1";
            this.tsbSnap.ButtonClick += new System.EventHandler(this.btnSnap_Click);
            // 
            // tsbClipboard
            // 
            this.tsbClipboard.Image = ((System.Drawing.Image)(resources.GetObject("tsbClipboard.Image")));
            this.tsbClipboard.Name = "tsbClipboard";
            this.tsbClipboard.Size = new System.Drawing.Size(180, 22);
            this.tsbClipboard.Text = "From Clipboard";
            this.tsbClipboard.Click += new System.EventHandler(this.tsbClipboard_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbOCR
            // 
            this.tsbOCR.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOCR.Image = ((System.Drawing.Image)(resources.GetObject("tsbOCR.Image")));
            this.tsbOCR.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbOCR.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOCR.Name = "tsbOCR";
            this.tsbOCR.Size = new System.Drawing.Size(28, 28);
            this.tsbOCR.Text = "toolStripButton1";
            this.tsbOCR.Click += new System.EventHandler(this.tsbOCR_Click);
            // 
            // tsbAutoOCR
            // 
            this.tsbAutoOCR.CheckOnClick = true;
            this.tsbAutoOCR.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAutoOCR.Image = ((System.Drawing.Image)(resources.GetObject("tsbAutoOCR.Image")));
            this.tsbAutoOCR.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbAutoOCR.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAutoOCR.Name = "tsbAutoOCR";
            this.tsbAutoOCR.Size = new System.Drawing.Size(28, 28);
            this.tsbAutoOCR.Text = "toolStripButton2";
            this.tsbAutoOCR.CheckedChanged += new System.EventHandler(this.tsbAutoOCR_CheckedChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbRevert
            // 
            this.tsbRevert.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRevert.Image = ((System.Drawing.Image)(resources.GetObject("tsbRevert.Image")));
            this.tsbRevert.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbRevert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRevert.Name = "tsbRevert";
            this.tsbRevert.Size = new System.Drawing.Size(28, 28);
            this.tsbRevert.Text = "Revert";
            this.tsbRevert.ToolTipText = "Clear selection";
            this.tsbRevert.Click += new System.EventHandler(this.btnRevert_Click);
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSaveTranslated,
            this.tsbSaveRaw});
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(40, 28);
            this.tsbSave.Text = "Save";
            this.tsbSave.ButtonClick += new System.EventHandler(this.btnSave_Click);
            // 
            // tsbSaveTranslated
            // 
            this.tsbSaveTranslated.Name = "tsbSaveTranslated";
            this.tsbSaveTranslated.Size = new System.Drawing.Size(183, 22);
            this.tsbSaveTranslated.Text = "Save with translation";
            this.tsbSaveTranslated.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tsbSaveRaw
            // 
            this.tsbSaveRaw.Name = "tsbSaveRaw";
            this.tsbSaveRaw.Size = new System.Drawing.Size(183, 22);
            this.tsbSaveRaw.Text = "Save raw screenshot";
            this.tsbSaveRaw.Click += new System.EventHandler(this.tsbSaveRaw_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbTextText
            // 
            this.tsbTextText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTextText.Image = ((System.Drawing.Image)(resources.GetObject("tsbTextText.Image")));
            this.tsbTextText.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbTextText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTextText.Name = "tsbTextText";
            this.tsbTextText.Size = new System.Drawing.Size(28, 28);
            this.tsbTextText.Text = "Text Input";
            this.tsbTextText.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // tsbSettings
            // 
            this.tsbSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSettings.Image = ((System.Drawing.Image)(resources.GetObject("tsbSettings.Image")));
            this.tsbSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSettings.Name = "tsbSettings";
            this.tsbSettings.Size = new System.Drawing.Size(28, 28);
            this.tsbSettings.Text = "Settings";
            this.tsbSettings.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // sfdDisplay
            // 
            this.sfdDisplay.DefaultExt = "png";
            this.sfdDisplay.Filter = "PNG|*.png";
            // 
            // txtPlaceholder
            // 
            this.txtPlaceholder.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtPlaceholder.BackColor = System.Drawing.SystemColors.Control;
            this.txtPlaceholder.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPlaceholder.Enabled = false;
            this.txtPlaceholder.Location = new System.Drawing.Point(194, 144);
            this.txtPlaceholder.Multiline = true;
            this.txtPlaceholder.Name = "txtPlaceholder";
            this.txtPlaceholder.ReadOnly = true;
            this.txtPlaceholder.ShortcutsEnabled = false;
            this.txtPlaceholder.Size = new System.Drawing.Size(100, 30);
            this.txtPlaceholder.TabIndex = 3;
            this.txtPlaceholder.Text = "Take a snapshot to get started";
            this.txtPlaceholder.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tsbVFW
            // 
            this.tsbVFW.CheckOnClick = true;
            this.tsbVFW.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbVFW.Image = ((System.Drawing.Image)(resources.GetObject("tsbVFW.Image")));
            this.tsbVFW.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbVFW.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbVFW.Name = "tsbVFW";
            this.tsbVFW.Size = new System.Drawing.Size(28, 28);
            this.tsbVFW.Text = "toolStripButton1";
            this.tsbVFW.Click += new System.EventHandler(this.tsbVFW_Click);
            // 
            // frmBabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(501, 338);
            this.Controls.Add(this.tscMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(517, 377);
            this.Name = "frmBabel";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Babel - Init";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Load += new System.EventHandler(this.Viewfinder_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmViewFinder_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmViewFinder_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pbxDisplay)).EndInit();
            this.tscMain.ContentPanel.ResumeLayout(false);
            this.tscMain.TopToolStripPanel.ResumeLayout(false);
            this.tscMain.TopToolStripPanel.PerformLayout();
            this.tscMain.ResumeLayout(false);
            this.tscMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pbxDisplay;
        private System.Windows.Forms.ToolStripContainer tscMain;
        private System.Windows.Forms.SaveFileDialog sfdDisplay;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbRevert;
        private System.Windows.Forms.ToolStripButton tsbSettings;
        private System.Windows.Forms.ToolStripSplitButton tsbSave;
        private System.Windows.Forms.ToolStripMenuItem tsbSaveRaw;
        private System.Windows.Forms.ToolStripMenuItem tsbSaveTranslated;
        private System.Windows.Forms.ToolStripButton tsbTextText;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSplitButton tsbSnap;
        private System.Windows.Forms.ToolStripMenuItem tsbClipboard;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbOCR;
        private System.Windows.Forms.ToolStripButton tsbAutoOCR;
        private System.Windows.Forms.TextBox txtPlaceholder;
        private System.Windows.Forms.ToolStripButton tsbVFW;
    }
}

