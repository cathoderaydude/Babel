using System;
using System.Windows.Forms;

namespace Babel
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/CathodeRayDude/Babel");
        }

        private void About_Load(object sender, EventArgs e)
        {
            txtVersion.Text = "Babel - Version " + frmBabel.AppVersion();
        }
    }
}
