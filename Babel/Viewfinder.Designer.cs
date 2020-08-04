namespace Babel
{
    partial class frmViewFinder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewFinder));
            this.pbxDisplay = new System.Windows.Forms.PictureBox();
            this.tscMain = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbSnap = new System.Windows.Forms.ToolStripButton();
            this.tsbRevert = new System.Windows.Forms.ToolStripButton();
            this.tsbClear = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbSettings = new System.Windows.Forms.ToolStripButton();
            this.sfdDisplay = new System.Windows.Forms.SaveFileDialog();
            this.bgwOCR = new System.ComponentModel.BackgroundWorker();
            this.bgwTranslate = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDisplay)).BeginInit();
            this.tscMain.ContentPanel.SuspendLayout();
            this.tscMain.TopToolStripPanel.SuspendLayout();
            this.tscMain.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbxDisplay
            // 
            this.pbxDisplay.BackColor = System.Drawing.Color.Magenta;
            this.pbxDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxDisplay.Location = new System.Drawing.Point(0, 0);
            this.pbxDisplay.Name = "pbxDisplay";
            this.pbxDisplay.Size = new System.Drawing.Size(921, 565);
            this.pbxDisplay.TabIndex = 1;
            this.pbxDisplay.TabStop = false;
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
            this.tscMain.ContentPanel.Controls.Add(this.pbxDisplay);
            this.tscMain.ContentPanel.Size = new System.Drawing.Size(921, 565);
            this.tscMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tscMain.Location = new System.Drawing.Point(5, 5);
            this.tscMain.Name = "tscMain";
            this.tscMain.Size = new System.Drawing.Size(921, 590);
            this.tscMain.TabIndex = 7;
            this.tscMain.Text = "toolStripContainer1";
            // 
            // tscMain.TopToolStripPanel
            // 
            this.tscMain.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSnap,
            this.tsbRevert,
            this.tsbClear,
            this.tsbSave,
            this.tsbSettings});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(118, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbSnap
            // 
            this.tsbSnap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSnap.Image = ((System.Drawing.Image)(resources.GetObject("tsbSnap.Image")));
            this.tsbSnap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSnap.Name = "tsbSnap";
            this.tsbSnap.Size = new System.Drawing.Size(23, 22);
            this.tsbSnap.Text = "Snap";
            this.tsbSnap.Click += new System.EventHandler(this.btnSnap_Click);
            // 
            // tsbRevert
            // 
            this.tsbRevert.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRevert.Image = ((System.Drawing.Image)(resources.GetObject("tsbRevert.Image")));
            this.tsbRevert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRevert.Name = "tsbRevert";
            this.tsbRevert.Size = new System.Drawing.Size(23, 22);
            this.tsbRevert.Text = "Revert";
            this.tsbRevert.ToolTipText = "Clear selection";
            this.tsbRevert.Click += new System.EventHandler(this.btnRevert_Click);
            // 
            // tsbClear
            // 
            this.tsbClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClear.Image = ((System.Drawing.Image)(resources.GetObject("tsbClear.Image")));
            this.tsbClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClear.Name = "tsbClear";
            this.tsbClear.Size = new System.Drawing.Size(23, 22);
            this.tsbClear.Text = "Clear";
            this.tsbClear.ToolTipText = "Clear image";
            this.tsbClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(23, 22);
            this.tsbSave.Text = "Save Image";
            this.tsbSave.ToolTipText = "Save image";
            this.tsbSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tsbSettings
            // 
            this.tsbSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSettings.Image = ((System.Drawing.Image)(resources.GetObject("tsbSettings.Image")));
            this.tsbSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSettings.Name = "tsbSettings";
            this.tsbSettings.Size = new System.Drawing.Size(23, 22);
            this.tsbSettings.Text = "Settings";
            this.tsbSettings.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // sfdDisplay
            // 
            this.sfdDisplay.DefaultExt = "png";
            // 
            // bgwOCR
            // 
            this.bgwOCR.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwOCR_DoWork);
            this.bgwOCR.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwOCR_RunWorkerCompleted);
            // 
            // bgwTranslate
            // 
            this.bgwTranslate.WorkerSupportsCancellation = true;
            this.bgwTranslate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwTranslate_DoWork);
            this.bgwTranslate.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwTranslate_RunWorkerCompleted);
            // 
            // frmViewFinder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 600);
            this.Controls.Add(this.tscMain);
            this.KeyPreview = true;
            this.Name = "frmViewFinder";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Viewfinder - Init";
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
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pbxDisplay;
        private System.Windows.Forms.ToolStripContainer tscMain;
        private System.Windows.Forms.SaveFileDialog sfdDisplay;
        private System.ComponentModel.BackgroundWorker bgwOCR;
        private System.ComponentModel.BackgroundWorker bgwTranslate;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbSnap;
        private System.Windows.Forms.ToolStripButton tsbRevert;
        private System.Windows.Forms.ToolStripButton tsbClear;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripButton tsbSettings;
    }
}

