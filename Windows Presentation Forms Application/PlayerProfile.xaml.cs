using Data_Layer.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Windows_Presentation_Forms_Application
{
    /// <summary>
    /// Interaction logic for PlayerProfile.xaml
    /// </summary>
    public partial class PlayerProfile : UserControl
    {
        private Player player;
        private ImageBrush playerImageBrush;
        public long Id { get; set; }
        public string FullName { get; set; }
        public PlayerProfile(Player player)
        {
            InitializeComponent();
            playerImageBrush = PlayerImage;
            this.player = player;
            Id = player.Id;
            FullName = player.Name;
        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog()
            {
                Filter = "Pictures|*.jpeg;*.jpg;*.png;",
                Multiselect = false,
                Title = $"Choose profile picture for {PlayerName}",
                InitialDirectory = Environment.CurrentDirectory
            };

            if (fileDialog.ShowDialog() == true) // File dialog has been opened and a file has been selected
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(fileDialog.FileName);
                bitmap.EndInit();

                // Assuming PlayerImage is an Image control
                PlayerImage.ImageSource = bitmap;
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            ShirtNumber.Content = Id;
            PlayerName.Content = FullName;
        }

        private void Grid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ScaleTransform scale = PlayerProfileScaleTransform;
            DoubleAnimation scaleXAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0.5,
                Duration = new Duration(TimeSpan.FromSeconds(0.5)),
                AutoReverse = true,
                RepeatBehavior = new RepeatBehavior(2),
                EasingFunction = new ElasticEase { Oscillations = 1, Springiness = 8 }
            };

            DoubleAnimation scaleYAnimation = new DoubleAnimation
            {
                From = 1,
                To = 1.5,
                Duration = new Duration(TimeSpan.FromSeconds(0.7)),
                AutoReverse = true,
                RepeatBehavior = new RepeatBehavior(2),
                EasingFunction = new BackEase { Amplitude = 0.5 }
            };

            scale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnimation);
            scale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleYAnimation);


            Image playerImage = new Image();
            playerImage.Source = playerImageBrush.ImageSource;

            PlayerInformation window = new PlayerInformation(player, playerImageBrush);
            window.ShowDialog();
        }

        private void PlayerImage_Changed(object sender, EventArgs e)
        {
            if (PlayerImage is not null)
            {
                playerImageBrush = PlayerImage;
            }
        }
    }
}
