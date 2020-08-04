namespace Babel
{
    partial class Controls
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
            this.pbxDisplay = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // pbxDisplay
            // 
            this.pbxDisplay.BackColor = System.Drawing.Color.Magenta;
            this.pbxDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxDisplay.Location = new System.Drawing.Point(0, 0);
            this.pbxDisplay.Name = "pbxDisplay";
            this.pbxDisplay.Size = new System.Drawing.Size(800, 450);
            this.pbxDisplay.TabIndex = 2;
            this.pbxDisplay.TabStop = false;
            // 
            // Controls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pbxDisplay);
            this.Name = "Controls";
            this.Text = "Viewfinder";
            ((System.ComponentModel.ISupportInitialize)(this.pbxDisplay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbxDisplay;
    }
}