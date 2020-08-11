using System;
using System.Windows.Forms;
using System.Linq;
using Babel.Async;

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
                txtGoogleKeyFile.Text = ofdKeyFile.FileName;
        }

        private void txtProjectName_TextChanged(object sender, EventArgs e)
        {

        }

        private DataSource DecodeDataSource(string value)
        {
            switch (value) {
                case "Google":
                    return DataSource.Google;
                case "Microsoft":
                    return DataSource.Microsoft;
                case "DeepL":
                    return DataSource.DeepL;
                default:
                    return DataSource.Dummy;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.targetLocale = (cmbLocale.SelectedItem == null) ? "en" : ((LanguageItem)cmbLocale.SelectedItem).code;
            Properties.Settings.Default.displayTimes = cbxDisplayTimes.Checked;

            Properties.Settings.Default.googleApiKeyPath = txtGoogleKeyFile.Text;
            Properties.Settings.Default.googleProjectName = txtGoogleProjectName.Text;

            Properties.Settings.Default.microsoftOcrApiKey = txtMsOcrApiKey.Text;
            Properties.Settings.Default.microsoftOcrEndpoint = txtMsOcrEndpoint.Text;
            Properties.Settings.Default.microsoftTranslatorApiKey = txtMsTranslatorApiKey.Text;

            Properties.Settings.Default.DeepLKey = txtDeepLKey.Text;

            Properties.Settings.Default.OCRDataSource = DecodeDataSource(cboOCR.Text);
            Properties.Settings.Default.TranslationDataSource = DecodeDataSource(cboTranslation.Text);

            if (Properties.Settings.Default.reqsPerSecond != (int)numRateLimit.Value)
            {
                Properties.Settings.Default.reqsPerSecond = (int)numRateLimit.Value;
                // Gotta reset this thing by hand
                AsyncStatic.rate.size = Properties.Settings.Default.reqsPerSecond;
            }

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
            if (gsl == null) gsl = AsyncStatic.MakeGSL(LoadLanguages);
            else LoadLanguages(gsl);

            txtGoogleKeyFile.Text = Properties.Settings.Default.googleApiKeyPath;
            txtGoogleProjectName.Text = Properties.Settings.Default.googleProjectName;

            txtMsOcrApiKey.Text = Properties.Settings.Default.microsoftOcrApiKey;
            txtMsOcrEndpoint.Text = Properties.Settings.Default.microsoftOcrEndpoint;
            txtMsTranslatorApiKey.Text = Properties.Settings.Default.microsoftTranslatorApiKey;

            txtDeepLKey.Text = Properties.Settings.Default.DeepLKey;

            switch (Properties.Settings.Default.OCRDataSource)
            {
                case DataSource.Google:
                    cboOCR.Text = "Google";
                    break;

                case DataSource.Microsoft:
                    cboOCR.Text = "Microsoft";
                    break;

                default:
                    cboOCR.Text = "Dummy";
                    break;
            }

            switch (Properties.Settings.Default.TranslationDataSource)
            {
                case DataSource.Google:
                    cboTranslation.Text = "Google";
                    break;

                case DataSource.Microsoft:
                    cboTranslation.Text = "Microsoft";
                    break;

                case DataSource.DeepL:
                    cboTranslation.Text = "DeepL";
                    break;

                default:
                    cboTranslation.Text = "Dummy";
                    break;
            }

            cbxDisplayTimes.Checked = Properties.Settings.Default.displayTimes;
            numRateLimit.Value = Properties.Settings.Default.reqsPerSecond;
        }

        private static IAsyncGSL gsl = null;

        private GSLCallback SafeLoadLanguages;
        private void LoadLanguages(IAsyncGSL gsl)
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
            gsl = AsyncStatic.MakeGSL(LoadLanguages);
        }
    }
}
