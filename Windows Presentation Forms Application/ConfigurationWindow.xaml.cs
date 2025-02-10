using Data_Layer.DAL;
using Data_Layer.Utilities;
using Data_Layer.Utilities.Windows_Forms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Windows_Presentation_Forms_Application
{
    /// <summary>
    /// Interaction logic for ConfigurationWindow.xaml
    /// </summary>
    public partial class ConfigurationWindow : Window
    {
        private Culture[] languages =
        {
            new Culture("English", CultureInfo.GetCultureInfo("en")),
            new Culture("Croatian", CultureInfo.GetCultureInfo("hr"))
        };

        private Resolution[] resolutions =
        {
            new Resolution("Fullscreen", 0, 0),
            new Resolution(1920, 1080),
            new Resolution(1024, 768),
            new Resolution(1280, 720),
        };

        private bool firstTime = true;

        private static int selectedIndexGender = 0;
        private static int selectedIndexLanguage = 0;
        private static int selectedIndexResolution = 0;

        private bool AllowChangeIndexEvent = false;

        public ConfigurationWindow(bool firstTime = true)
        {
            InitializeComponent();
            InitializeConfiguration();
            this.firstTime = firstTime;
        }

        private void InitializeConfiguration()
        {
            InitializeComboBoxes();
        }

        private void InitializeComboBoxes()
        {
            InitializeGenderComboBox();
            InitializeLanguageComboBox();
            InitializeResolutionComboBox();
            AllowChangeIndexEvent = true;
        }

        private void InitializeResolutionComboBox()
        {
            foreach (var resolution in resolutions)
            {
                CbResolution.Items.Add(resolution);
            }

            CbResolution.SelectedIndex = selectedIndexResolution;
        }

        private void InitializeLanguageComboBox()
        {
            foreach (var language in languages)
            {
                CbLanguage.Items.Add(language);
            }

            CbLanguage.SelectedIndex = selectedIndexLanguage;
        }

        private void InitializeGenderComboBox()
        {
            var genders = Enum.GetNames(typeof(IData.Gender));

            foreach (var gender in genders)
            {
                CbGender.Items.Add(gender);
            }

            CbGender.SelectedIndex = selectedIndexGender;
        }

        private void CbGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedIndexGender = CbGender.SelectedIndex;
        }

        private void CbLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                selectedIndexLanguage = CbLanguage.SelectedIndex;
                
                if (!AllowChangeIndexEvent)
                {
                    return;
                }

                var culture = CbLanguage.SelectedItem as Culture;

                if (culture != null)
                {
                    ConfigUtils.SetSetting("Language", culture.Value.Name);
                }
            }
            catch 
            {
                MessageBox.Show("Failed to load language.", "Language error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CbResolution_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                selectedIndexResolution = CbResolution.SelectedIndex;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var resolution = CbResolution.SelectedItem as Resolution;

                if (resolution != null)
                {
                    ConfigUtils.SetSetting("Resolution", resolution.ResolutionScale);
                    ConfigUtils.SetSetting("Priority", CbGender.SelectedItem.ToString() ?? "Men");
                }

                DialogResult = true;
                Close();
            }
            catch 
            {
                MessageBox.Show("Failed to initialize settings.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (firstTime)
            {
                Environment.Exit(0);
            }

            DialogResult = false;
            Close();
        }
    }
}
