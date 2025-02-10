using Data_Layer.DAL;
using Data_Layer.Models;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static Data_Layer.DAL.IData;

namespace Windows_Presentation_Forms_Application
{
    /// <summary>
    /// Interaction logic for PlayerInformation.xaml
    /// </summary>
    public partial class PlayerInformation : Window
    {
        public PlayerInformation(Player player, ImageBrush image)
        {
            InitializeComponent();
            PlayerImage.ImageSource = image.ImageSource;
            LabelId.Content = player.Id;
            LabelName.Content = player.Name;
            LabelPosition.Content = player.Position;
            LabelCaptain.Content = player.Captain ? "Yes" : "No";
        }
    }
}
