using System;
using System.Windows.Forms;

namespace Babel
{
    public partial class Disclaimer : Form
    {
        public Disclaimer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DebugLog.Log("User declined waiver. Exiting.");
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DebugLog.Log("User agreed to waiver.");
            Properties.Settings.Default.WaiverSigned = true;
            Properties.Settings.Default.Save();
            this.Close();
        }
    }
}
