using Data_Layer.DAL;
using Data_Layer.Utilities;
using Data_Layer.Utilities.Windows_Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Data_Layer.DAL.IData;
using static System.Windows.Forms.DataFormats;

namespace Windows_Forms_Application
{
    public partial class ConfigurationForm : Form
    {
        private Culture[] languages =
        {
            new Culture("English", CultureInfo.GetCultureInfo("en")),
            new Culture("Croatian", CultureInfo.GetCultureInfo("hr"))
        };
        private static bool AllowChangeIndexEvent = false;
        private bool firstTime;

        private IData.Gender? currentGender;
        private Culture? currentCulture;

        private static int currentGenderIndex;
        private static int currentLanguageIndex;
        public ConfigurationForm(bool firstTime, IData.Gender? gender = null, Culture? currentCulture = null)
        {
            this.firstTime = firstTime;
            UpdateSettingConfiguration(gender, currentCulture);
            LoadStandardLanguage();
            InitializeComponent();
            InitializeConfiguration();
        }

        private void UpdateSettingConfiguration(Gender? gender, Culture? culture)
        {
            if (gender is not null)
            {
                currentGender = gender;
            }

            if (culture is not null)
            {
                currentCulture = culture;
            }
        }

        private void LoadStandardLanguage()
        {
            if (currentCulture is not null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(currentCulture.Value.TwoLetterISOLanguageName);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(currentCulture.Value.TwoLetterISOLanguageName);
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            }
        }
        private void InitializeConfiguration()
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            this.CenterToScreen();
        }
        private void ConfigurationForm_Load(object sender, EventArgs e)
        {
            InitializeGenderComboBox();
            InitializeLanguageComboBox();
            AllowChangeIndexEvent = true;
        }
        private void InitializeGenderComboBox()
        {
            CbGender.Items.AddRange(Enum.GetNames(typeof(IData.Gender)));

            CbGender.SelectedIndex = currentGender is not null ? CbGender.Items.IndexOf(currentGender.ToString()) : currentGenderIndex;
        }
        private void InitializeLanguageComboBox()
        {
            foreach (var language in languages)
            {
                CbLanguage.Items.Add(language);
            }

            CbLanguage.SelectedIndex = currentLanguageIndex;
        }
        private void CbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                currentLanguageIndex = CbLanguage.SelectedIndex;
                if (!AllowChangeIndexEvent)
                {
                    return;
                }
                var culture = CbLanguage.SelectedItem as Culture;
                if (culture is not null && culture is Culture)
                {
                    ConfigUtils.SetSetting("Language", culture.Value.Name);
                    ChangeTextTo(culture);
                }
            }
            catch
            {
                MessageBox.Show("Error has occured in trying to select language for the application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ChangeTextTo(Culture culture)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(culture.Value.Name);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture.Value.Name);

                ResetEverything();
            }
            catch (Exception)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            }
        }
        private void ResetEverything()
        {
            Controls.Clear();
            InitializeComponent();
            AllowChangeIndexEvent = false;
            InitializeGenderComboBox();
            InitializeLanguageComboBox();
            AllowChangeIndexEvent = true;
        }
        private void CbGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentGenderIndex = CbGender.SelectedIndex;
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save these settings?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    Environment.Exit(0);
                    return;
                }

                ConfigUtils.SetSetting("Priority", CbGender.SelectedItem.ToString() ?? "Men");
            }
            catch
            {
                MessageBox.Show("Error has occured in trying to select gender for application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (firstTime)
            {
                Environment.Exit(0);
            }
        }
    }
}
