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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBabel));
            this.pbxDisplay = new System.Windows.Forms.PictureBox();
            this.tscMain = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusBarLeft = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBarRight = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtPlaceholder = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbSnap = new System.Windows.Forms.ToolStripSplitButton();
            this.tsbClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbSave = new System.Windows.Forms.ToolStripSplitButton();
            this.tsbSaveTranslated = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbSaveRaw = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbVFW = new System.Windows.Forms.ToolStripSplitButton();
            this.tsbVFWAutoSize = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbMaxVFW = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbCrosshair = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbOCR = new System.Windows.Forms.ToolStripButton();
            this.tsbAutoOCR = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAutophrase = new System.Windows.Forms.ToolStripButton();
            this.tsbAutoAutophrase = new System.Windows.Forms.ToolStripButton();
            this.tsbAutofit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbIntersectsMode = new System.Windows.Forms.ToolStripButton();
            this.tsbContainsMode = new System.Windows.Forms.ToolStripButton();
            this.tsbRevert = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbTextText = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSettings = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.sfdDisplay = new System.Windows.Forms.SaveFileDialog();
            this.ctxPhrase = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyTranslatedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyOriginalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyBothToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.alignToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leftEdgesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightEdgesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topEdgesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bottomEdgesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.horizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.distributeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verticallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.horizontallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDisplay)).BeginInit();
            this.tscMain.BottomToolStripPanel.SuspendLayout();
            this.tscMain.ContentPanel.SuspendLayout();
            this.tscMain.TopToolStripPanel.SuspendLayout();
            this.tscMain.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.ctxPhrase.SuspendLayout();
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
            this.pbxDisplay.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pbxDisplay_MouseDoubleClick);
            this.pbxDisplay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbxDisplay_MouseDown);
            this.pbxDisplay.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbxDisplay_MouseMove);
            this.pbxDisplay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbxDisplay_MouseUp);
            // 
            // tscMain
            // 
            // 
            // tscMain.BottomToolStripPanel
            // 
            this.tscMain.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // tscMain.ContentPanel
            // 
            this.tscMain.ContentPanel.AutoScroll = true;
            this.tscMain.ContentPanel.Controls.Add(this.panel1);
            this.tscMain.ContentPanel.Padding = new System.Windows.Forms.Padding(5);
            this.tscMain.ContentPanel.Size = new System.Drawing.Size(591, 364);
            this.tscMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tscMain.Location = new System.Drawing.Point(0, 0);
            this.tscMain.Name = "tscMain";
            this.tscMain.Size = new System.Drawing.Size(591, 417);
            this.tscMain.TabIndex = 7;
            this.tscMain.Text = "toolStripContainer1";
            // 
            // tscMain.TopToolStripPanel
            // 
            this.tscMain.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBarLeft,
            this.toolStripStatusLabel2,
            this.statusBarRight});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(591, 22);
            this.statusStrip1.TabIndex = 0;
            // 
            // statusBarLeft
            // 
            this.statusBarLeft.Name = "statusBarLeft";
            this.statusBarLeft.Size = new System.Drawing.Size(57, 17);
            this.statusBarLeft.Text = "Initialized";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.IsLink = true;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(409, 17);
            this.toolStripStatusLabel2.Spring = true;
            this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // statusBarRight
            // 
            this.statusBarRight.Name = "statusBarRight";
            this.statusBarRight.Size = new System.Drawing.Size(110, 17);
            this.statusBarRight.Text = "Usage: 1,000/52,523";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.txtPlaceholder);
            this.panel1.Controls.Add(this.pbxDisplay);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(581, 354);
            this.panel1.TabIndex = 2;
            // 
            // txtPlaceholder
            // 
            this.txtPlaceholder.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtPlaceholder.BackColor = System.Drawing.SystemColors.Control;
            this.txtPlaceholder.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPlaceholder.Enabled = false;
            this.txtPlaceholder.Location = new System.Drawing.Point(232, 214);
            this.txtPlaceholder.Multiline = true;
            this.txtPlaceholder.Name = "txtPlaceholder";
            this.txtPlaceholder.ReadOnly = true;
            this.txtPlaceholder.ShortcutsEnabled = false;
            this.txtPlaceholder.Size = new System.Drawing.Size(100, 30);
            this.txtPlaceholder.TabIndex = 3;
            this.txtPlaceholder.Text = "Take a snapshot to get started";
            this.txtPlaceholder.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSnap,
            this.tsbSave,
            this.toolStripSeparator8,
            this.tsbVFW,
            this.tsbCrosshair,
            this.toolStripSeparator3,
            this.tsbOCR,
            this.tsbAutoOCR,
            this.toolStripSeparator1,
            this.tsbAutophrase,
            this.tsbAutoAutophrase,
            this.toolStripSeparator6,
            this.tsbAutofit,
            this.toolStripSeparator9,
            this.tsbIntersectsMode,
            this.tsbContainsMode,
            this.tsbRevert,
            this.toolStripSeparator2,
            this.tsbTextText,
            this.toolStripSeparator7,
            this.tsbSettings,
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(562, 31);
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
            this.tsbSnap.Text = "Take picture";
            this.tsbSnap.ButtonClick += new System.EventHandler(this.btnSnap_Click);
            // 
            // tsbClipboard
            // 
            this.tsbClipboard.Image = ((System.Drawing.Image)(resources.GetObject("tsbClipboard.Image")));
            this.tsbClipboard.Name = "tsbClipboard";
            this.tsbClipboard.Size = new System.Drawing.Size(188, 30);
            this.tsbClipboard.Text = "Paste image";
            this.tsbClipboard.Click += new System.EventHandler(this.tsbClipboard_Click);
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
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbVFW
            // 
            this.tsbVFW.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbVFW.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbVFWAutoSize,
            this.tsbMaxVFW});
            this.tsbVFW.Image = ((System.Drawing.Image)(resources.GetObject("tsbVFW.Image")));
            this.tsbVFW.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbVFW.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbVFW.Name = "tsbVFW";
            this.tsbVFW.Size = new System.Drawing.Size(40, 28);
            this.tsbVFW.Text = "Show viewfinder";
            this.tsbVFW.ButtonClick += new System.EventHandler(this.tsbVFW_Click);
            // 
            // tsbVFWAutoSize
            // 
            this.tsbVFWAutoSize.CheckOnClick = true;
            this.tsbVFWAutoSize.Image = ((System.Drawing.Image)(resources.GetObject("tsbVFWAutoSize.Image")));
            this.tsbVFWAutoSize.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbVFWAutoSize.Name = "tsbVFWAutoSize";
            this.tsbVFWAutoSize.Size = new System.Drawing.Size(226, 30);
            this.tsbVFWAutoSize.Text = "Scale viewfinder to window";
            this.tsbVFWAutoSize.Click += new System.EventHandler(this.scaleViewfinderToWindowToolStripMenuItem_Click);
            // 
            // tsbMaxVFW
            // 
            this.tsbMaxVFW.Image = ((System.Drawing.Image)(resources.GetObject("tsbMaxVFW.Image")));
            this.tsbMaxVFW.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbMaxVFW.Name = "tsbMaxVFW";
            this.tsbMaxVFW.Size = new System.Drawing.Size(226, 30);
            this.tsbMaxVFW.Text = "Maximize viewfinder";
            this.tsbMaxVFW.Click += new System.EventHandler(this.tsbMaxVFW_Click);
            // 
            // tsbCrosshair
            // 
            this.tsbCrosshair.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCrosshair.Image = ((System.Drawing.Image)(resources.GetObject("tsbCrosshair.Image")));
            this.tsbCrosshair.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbCrosshair.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCrosshair.Name = "tsbCrosshair";
            this.tsbCrosshair.Size = new System.Drawing.Size(28, 28);
            this.tsbCrosshair.Text = "Window picker";
            this.tsbCrosshair.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tsbCrosshair_MouseDown);
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
            this.tsbOCR.Size = new System.Drawing.Size(30, 28);
            this.tsbOCR.Text = "Recognize words";
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
            this.tsbAutoOCR.Text = "Auto-recognize words";
            this.tsbAutoOCR.CheckedChanged += new System.EventHandler(this.tsbAutoOCR_CheckedChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbAutophrase
            // 
            this.tsbAutophrase.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAutophrase.Image = ((System.Drawing.Image)(resources.GetObject("tsbAutophrase.Image")));
            this.tsbAutophrase.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbAutophrase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAutophrase.Name = "tsbAutophrase";
            this.tsbAutophrase.Size = new System.Drawing.Size(28, 28);
            this.tsbAutophrase.Text = "Autophrase";
            this.tsbAutophrase.Click += new System.EventHandler(this.tsbAutophrase_Click);
            // 
            // tsbAutoAutophrase
            // 
            this.tsbAutoAutophrase.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAutoAutophrase.Image = ((System.Drawing.Image)(resources.GetObject("tsbAutoAutophrase.Image")));
            this.tsbAutoAutophrase.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbAutoAutophrase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAutoAutophrase.Name = "tsbAutoAutophrase";
            this.tsbAutoAutophrase.Size = new System.Drawing.Size(28, 28);
            this.tsbAutoAutophrase.Text = "Automatic autophrase";
            this.tsbAutoAutophrase.Click += new System.EventHandler(this.tsbAutoAutophrase_Click);
            // 
            // tsbAutofit
            // 
            this.tsbAutofit.Checked = true;
            this.tsbAutofit.CheckOnClick = true;
            this.tsbAutofit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbAutofit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAutofit.Image = ((System.Drawing.Image)(resources.GetObject("tsbAutofit.Image")));
            this.tsbAutofit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAutofit.Name = "tsbAutofit";
            this.tsbAutofit.Size = new System.Drawing.Size(28, 28);
            this.tsbAutofit.Text = "Fit phrases to matching words";
            this.tsbAutofit.Click += new System.EventHandler(this.tsbAutofit_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbIntersectsMode
            // 
            this.tsbIntersectsMode.Checked = true;
            this.tsbIntersectsMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbIntersectsMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbIntersectsMode.Image = ((System.Drawing.Image)(resources.GetObject("tsbIntersectsMode.Image")));
            this.tsbIntersectsMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbIntersectsMode.Name = "tsbIntersectsMode";
            this.tsbIntersectsMode.Size = new System.Drawing.Size(28, 28);
            this.tsbIntersectsMode.Text = "Intersect mode";
            this.tsbIntersectsMode.Click += new System.EventHandler(this.tsbIntersectsMode_Click);
            // 
            // tsbContainsMode
            // 
            this.tsbContainsMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbContainsMode.Image = ((System.Drawing.Image)(resources.GetObject("tsbContainsMode.Image")));
            this.tsbContainsMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbContainsMode.Name = "tsbContainsMode";
            this.tsbContainsMode.Size = new System.Drawing.Size(28, 28);
            this.tsbContainsMode.Text = "Contains mode";
            this.tsbContainsMode.Click += new System.EventHandler(this.tsbContainsMode_Click);
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
            this.tsbRevert.ToolTipText = "Clear phrases";
            this.tsbRevert.Click += new System.EventHandler(this.btnRevert_Click);
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
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 31);
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
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(28, 28);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(28, 28);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // sfdDisplay
            // 
            this.sfdDisplay.DefaultExt = "png";
            this.sfdDisplay.Filter = "PNG|*.png";
            // 
            // ctxPhrase
            // 
            this.ctxPhrase.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyTranslatedToolStripMenuItem,
            this.copyOriginalToolStripMenuItem,
            this.copyBothToolStripMenuItem,
            this.toolStripSeparator4,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator5,
            this.alignToolStripMenuItem,
            this.fitToolStripMenuItem,
            this.distributeToolStripMenuItem});
            this.ctxPhrase.Name = "ctxPhrase";
            this.ctxPhrase.Size = new System.Drawing.Size(159, 170);
            this.ctxPhrase.Opening += new System.ComponentModel.CancelEventHandler(this.ctxPhrase_Opening);
            // 
            // copyTranslatedToolStripMenuItem
            // 
            this.copyTranslatedToolStripMenuItem.Name = "copyTranslatedToolStripMenuItem";
            this.copyTranslatedToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.copyTranslatedToolStripMenuItem.Text = "Copy Translated";
            this.copyTranslatedToolStripMenuItem.Click += new System.EventHandler(this.copyTranslatedToolStripMenuItem_Click);
            // 
            // copyOriginalToolStripMenuItem
            // 
            this.copyOriginalToolStripMenuItem.Name = "copyOriginalToolStripMenuItem";
            this.copyOriginalToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.copyOriginalToolStripMenuItem.Text = "Copy Original";
            this.copyOriginalToolStripMenuItem.Click += new System.EventHandler(this.copyOriginalToolStripMenuItem_Click);
            // 
            // copyBothToolStripMenuItem
            // 
            this.copyBothToolStripMenuItem.Name = "copyBothToolStripMenuItem";
            this.copyBothToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.copyBothToolStripMenuItem.Text = "Copy Both";
            this.copyBothToolStripMenuItem.Click += new System.EventHandler(this.copyBothToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(155, 6);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(155, 6);
            // 
            // alignToolStripMenuItem
            // 
            this.alignToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.leftEdgesToolStripMenuItem,
            this.rightEdgesToolStripMenuItem,
            this.topEdgesToolStripMenuItem,
            this.bottomEdgesToolStripMenuItem});
            this.alignToolStripMenuItem.Name = "alignToolStripMenuItem";
            this.alignToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.alignToolStripMenuItem.Text = "Align";
            // 
            // leftEdgesToolStripMenuItem
            // 
            this.leftEdgesToolStripMenuItem.Name = "leftEdgesToolStripMenuItem";
            this.leftEdgesToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.leftEdgesToolStripMenuItem.Text = "Left edges";
            this.leftEdgesToolStripMenuItem.Click += new System.EventHandler(this.alignLeftEdgesToolStripMenuItem_Click);
            // 
            // rightEdgesToolStripMenuItem
            // 
            this.rightEdgesToolStripMenuItem.Name = "rightEdgesToolStripMenuItem";
            this.rightEdgesToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.rightEdgesToolStripMenuItem.Text = "Right edges";
            this.rightEdgesToolStripMenuItem.Click += new System.EventHandler(this.rightEdgesToolStripMenuItem_Click);
            // 
            // topEdgesToolStripMenuItem
            // 
            this.topEdgesToolStripMenuItem.Name = "topEdgesToolStripMenuItem";
            this.topEdgesToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.topEdgesToolStripMenuItem.Text = "Top edges";
            this.topEdgesToolStripMenuItem.Click += new System.EventHandler(this.topEdgesToolStripMenuItem_Click);
            // 
            // bottomEdgesToolStripMenuItem
            // 
            this.bottomEdgesToolStripMenuItem.Name = "bottomEdgesToolStripMenuItem";
            this.bottomEdgesToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.bottomEdgesToolStripMenuItem.Text = "Bottom edges";
            this.bottomEdgesToolStripMenuItem.Click += new System.EventHandler(this.bottomEdgesToolStripMenuItem_Click);
            // 
            // fitToolStripMenuItem
            // 
            this.fitToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.horizontalToolStripMenuItem,
            this.verticalToolStripMenuItem});
            this.fitToolStripMenuItem.Name = "fitToolStripMenuItem";
            this.fitToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.fitToolStripMenuItem.Text = "Fit";
            // 
            // horizontalToolStripMenuItem
            // 
            this.horizontalToolStripMenuItem.Name = "horizontalToolStripMenuItem";
            this.horizontalToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.horizontalToolStripMenuItem.Text = "Horizontal";
            this.horizontalToolStripMenuItem.Click += new System.EventHandler(this.horizontalToolStripMenuItem_Click);
            // 
            // verticalToolStripMenuItem
            // 
            this.verticalToolStripMenuItem.Name = "verticalToolStripMenuItem";
            this.verticalToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.verticalToolStripMenuItem.Text = "Vertical";
            this.verticalToolStripMenuItem.Click += new System.EventHandler(this.verticalToolStripMenuItem_Click);
            // 
            // distributeToolStripMenuItem
            // 
            this.distributeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verticallyToolStripMenuItem,
            this.horizontallyToolStripMenuItem});
            this.distributeToolStripMenuItem.Name = "distributeToolStripMenuItem";
            this.distributeToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.distributeToolStripMenuItem.Text = "Distribute";
            // 
            // verticallyToolStripMenuItem
            // 
            this.verticallyToolStripMenuItem.Name = "verticallyToolStripMenuItem";
            this.verticallyToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.verticallyToolStripMenuItem.Text = "Vertically";
            this.verticallyToolStripMenuItem.Click += new System.EventHandler(this.verticallyToolStripMenuItem_Click);
            // 
            // horizontallyToolStripMenuItem
            // 
            this.horizontallyToolStripMenuItem.Name = "horizontallyToolStripMenuItem";
            this.horizontallyToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.horizontallyToolStripMenuItem.Text = "Horizontally";
            this.horizontallyToolStripMenuItem.Click += new System.EventHandler(this.horizontallyToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 31);
            // 
            // frmBabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(591, 417);
            this.Controls.Add(this.tscMain);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(517, 377);
            this.Name = "frmBabel";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Babel - Init";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Load += new System.EventHandler(this.Viewfinder_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmViewFinder_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmViewFinder_KeyUp);
            this.Resize += new System.EventHandler(this.frmBabel_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pbxDisplay)).EndInit();
            this.tscMain.BottomToolStripPanel.ResumeLayout(false);
            this.tscMain.BottomToolStripPanel.PerformLayout();
            this.tscMain.ContentPanel.ResumeLayout(false);
            this.tscMain.TopToolStripPanel.ResumeLayout(false);
            this.tscMain.TopToolStripPanel.PerformLayout();
            this.tscMain.ResumeLayout(false);
            this.tscMain.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ctxPhrase.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripSplitButton tsbVFW;
        private System.Windows.Forms.ToolStripMenuItem tsbVFWAutoSize;
        private System.Windows.Forms.ToolStripMenuItem tsbMaxVFW;
        private System.Windows.Forms.ToolStripButton tsbCrosshair;
        private System.Windows.Forms.ContextMenuStrip ctxPhrase;
        private System.Windows.Forms.ToolStripMenuItem copyTranslatedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyOriginalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyBothToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem alignToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem leftEdgesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rightEdgesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem topEdgesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bottomEdgesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem horizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem distributeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verticallyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem horizontallyToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusBarLeft;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel statusBarRight;
        private System.Windows.Forms.ToolStripButton tsbAutophrase;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tsbAutoAutophrase;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton tsbAutofit;
        private System.Windows.Forms.ToolStripButton tsbIntersectsMode;
        private System.Windows.Forms.ToolStripButton tsbContainsMode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
    }
}

