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

        private static ComboBoxItem<DataSource> dummy => new ComboBoxItem<DataSource>
        {
            name = "Dummy",
            data = DataSource.Dummy,
        };

        private static ComboBoxItem<DataSource> google => new ComboBoxItem<DataSource>
        {
            name = "Google",
            data = DataSource.Google,
        };

        private static ComboBoxItem<DataSource> microsoft => new ComboBoxItem<DataSource>
        {
            name = "Microsoft",
            data = DataSource.Microsoft,
        };

        private static ComboBoxItem<DataSource> deepl => new ComboBoxItem<DataSource>
        {
            name = "DeepL",
            data = DataSource.DeepL,
        };

        private static ComboBoxItem<DataSource>[] ocrSources =
        {
            dummy,
            google,
            microsoft,
        };

        private static ComboBoxItem<DataSource>[] translationSources =
        {
            dummy,
            google,
            microsoft,
            deepl,
        };

        private void btnOk_Click(object sender, EventArgs e)
        {
            // (object as type).member pattern will null-reference on accessing the member if
            // * object was null
            // * object could not be converted to type
            //
            // (object as type)?.member uses the object?.member syntax, so that if object is null,
            // instead of null-referencing, we just also return a null. this only works for nullable
            // types; strings are nullable, 
            Properties.Settings.Default.targetLocale = cmbLocale.SelectedData<string>();
            Properties.Settings.Default.displayTimes = cbxDisplayTimes.Checked;

            Properties.Settings.Default.googleApiKeyPath = txtGoogleKeyFile.Text;
            Properties.Settings.Default.googleProjectName = txtGoogleProjectName.Text;

            Properties.Settings.Default.microsoftOcrApiKey = txtMsOcrApiKey.Text;
            Properties.Settings.Default.microsoftOcrEndpoint = txtMsOcrEndpoint.Text;
            Properties.Settings.Default.microsoftTranslatorApiKey = txtMsTranslatorApiKey.Text;

            Properties.Settings.Default.DeepLKey = txtDeepLKey.Text;

            Properties.Settings.Default.OCRDataSource = cboOCR.SelectedData<DataSource>();
            Properties.Settings.Default.TranslationDataSource = cboTranslation.SelectedData<DataSource>();

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

            cboOCR.Items.Clear();
            cboOCR.Items.AddRange(ocrSources);
            cboOCR.SelectedItem = cboOCR.Items
                .Cast<ComboBoxItem<DataSource>>()
                .First(item => item.data == Properties.Settings.Default.OCRDataSource);

            cboTranslation.Items.Clear();
            cboTranslation.Items.AddRange(translationSources);
            cboTranslation.SelectedItem = cboTranslation.Items
                .Cast<ComboBoxItem<DataSource>>()
                .First(item => item.data == Properties.Settings.Default.TranslationDataSource);

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
                        .FirstOrDefault(item => item.data == Properties.Settings.Default.targetLocale);
                }
                else
                {
                    cmbLocale.SelectedText = Properties.Settings.Default.targetLocale;
                }
            }
        }

        // This setting needs to be split across each translation data source.
        // Some re-engineering may be involved.
        private void btnRefreshGSL_Click(object sender, EventArgs e)
        {
            gsl = AsyncStatic.MakeGSL(LoadLanguages);
        }
    }
}
