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

namespace Windows_Presentation_Forms_Application
{
    /// <summary>
    /// Interaction logic for PlayerInformation.xaml
    /// </summary>
    public partial class TeamInformation : Window
    {
        /* name, FIFA code, number of
games/wins/losses/draws, goals scored/conceived/ difference.*/
        public long? Id { get; set; }
        public string? TeamName { get; set; }
        public string? FifaCode { get; set; }
        public long? GroupId { get; set; }
        public string? GroupLetter { get; set; }
        public TeamInformation(Team team)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Id = team.Id;
            TeamName = team.Name;
            FifaCode = team.FifaCode;
            GroupId = team.GroupId;
            GroupLetter = team.GroupLetter;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            LabelId.Content = Id;
            LabelName.Content = TeamName;
            LabelCode.Content = FifaCode;
            LabelGroupLetter.Content = GroupLetter; 
            LabelGroupId.Content = GroupId;
        }
    }
}
