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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void txtKeyFile_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnBrowseKeyFile_Click(object sender, EventArgs e)
        {
            if (ofdKeyFile.ShowDialog() == DialogResult.OK)
                txtKeyFile.Text = ofdKeyFile.FileName;
        }

        private void txtProjectName_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.targetLocale = cmbLocale.Text;
            Properties.Settings.Default.apiKeyPath = txtKeyFile.Text;
            Properties.Settings.Default.projectName = txtProjectName.Text;
            Properties.Settings.Default.dummyData = cbxDummy.Checked;
            Properties.Settings.Default.displayTimes = cbxDisplayTimes.Checked;
            Properties.Settings.Default.autoOCR = cbxAutoOCR.Checked;

            Properties.Settings.Default.Save();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Settings_VisibleChanged(object sender, EventArgs e)
        {
            
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            cmbLocale.Text = Properties.Settings.Default.targetLocale;
            txtKeyFile.Text = Properties.Settings.Default.apiKeyPath;
            txtProjectName.Text = Properties.Settings.Default.projectName;
            cbxDummy.Checked = Properties.Settings.Default.dummyData;
            cbxDisplayTimes.Checked = Properties.Settings.Default.displayTimes;
            cbxAutoOCR.Checked = Properties.Settings.Default.autoOCR;
        }
    }
}
