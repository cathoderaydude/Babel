using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Babel
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            txtKeyFile.Text = Properties.Settings.Default.apiKeyPath;
            txtProjectName.Text = Properties.Settings.Default.projectName;
        }

        private void txtKeyFile_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.apiKeyPath = txtKeyFile.Text;
        }

        private void txtProjectName_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.projectName = txtProjectName.Text;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnBrowseKeyFile_Click(object sender, EventArgs e)
        {
            if (ofdKeyFile.ShowDialog() == DialogResult.OK)
                txtKeyFile.Text = ofdKeyFile.FileName;
        }
    }
}
