using Babel.Google;
using System;
using System.Windows.Forms;
using System.Linq;

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
            Properties.Settings.Default.targetLocale = ((LanguageItem)cmbLocale.SelectedItem).code;
            Properties.Settings.Default.apiKeyPath = txtKeyFile.Text;
            Properties.Settings.Default.projectName = txtProjectName.Text;
            Properties.Settings.Default.dummyData = cbxDummy.Checked;
            Properties.Settings.Default.displayTimes = cbxDisplayTimes.Checked;
            Properties.Settings.Default.autoOCR = cbxAutoOCR.Checked;
            Properties.Settings.Default.reqsPerSecond = (int)numRateLimit.Value;
            // Gotta reset this thing by hand
            Google.GoogleAsyncStatic.rate.size = Properties.Settings.Default.reqsPerSecond;

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
            SafeLoadLanguages = LoadLanguages;
            if (gsl == null) gsl = new AsyncGSL(LoadLanguages);
            else LoadLanguages(gsl);

            txtKeyFile.Text = Properties.Settings.Default.apiKeyPath;
            txtProjectName.Text = Properties.Settings.Default.projectName;
            cbxDummy.Checked = Properties.Settings.Default.dummyData;
            cbxDisplayTimes.Checked = Properties.Settings.Default.displayTimes;
            cbxAutoOCR.Checked = Properties.Settings.Default.autoOCR;
            numRateLimit.Value = Properties.Settings.Default.reqsPerSecond;
        }

        private static AsyncGSL gsl = null;

        private Action<AsyncGSL> SafeLoadLanguages;
        private void LoadLanguages(AsyncGSL gsl)
        {
            if (InvokeRequired)
            {
                Invoke(SafeLoadLanguages, new object[] { gsl });
            }
            else
            {
                cmbLocale.Items.Clear();
                cmbLocale.Enabled = gsl.isDone;
                if (gsl.isDone)
                {
                    cmbLocale.Items.AddRange(gsl.languages);
                    cmbLocale.SelectedItem = cmbLocale.Items
                        .Cast<LanguageItem>()
                        .FirstOrDefault(item => item.code == Properties.Settings.Default.targetLocale);
                }
                else
                {
                    cmbLocale.SelectedText = Properties.Settings.Default.targetLocale;
                }
            }
        }

        private void btnRefreshGSL_Click(object sender, EventArgs e)
        {
            gsl = new AsyncGSL(LoadLanguages);
        }
    }
}
