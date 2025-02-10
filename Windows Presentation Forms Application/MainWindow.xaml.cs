using Data_Layer.DAL;
using Data_Layer.Models;
using Data_Layer.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IData data;
        private IData.Gender currentGender;
        private Match currentMatch;

        private bool ChangeIndexEvent = true;
        public MainWindow()
        {
            InitializeForm();
        }

        private void InitializeForm()
        {
            try
            {
                ShowConfigurationForm();
                InitializeApplication();
                InitializeComboBox();
            }
            catch 
            {
                MessageBox.Show("Failed to load configuration.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
            }
        }

        private void ShowConfigurationForm()
        {
            try
            {
                if (File.Exists(ConfigUtils.filePath))
                {
                    return;
                }

                ConfigurationWindow window = new ConfigurationWindow();

                window.ShowDialog();
            }
            catch 
            {
                MessageBox.Show("Failed to display configuration form.", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            }
        }

        private void InitializeApplication()
        {
            try
            {
                data = DataFactory.GetData();
                LoadCulture();
                InitializeComponent();

                LoadGender();
                LoadResolution();
                ClearInformation();
            }
            catch 
            {
                MessageBox.Show("Failed to initialize application.", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                Environment.Exit(0);
            }
        }

        private async void InitializeComboBox()
        {
            var teams = await data.GetTeams(currentGender);

            foreach (var team in teams)
            {
                CbTeam.Items.Add(team);
            }

            SetTeamSelectedIndex(teams);
        }

        private void SetTeamSelectedIndex(List<Team> teams)
        {
            if (currentGender == IData.Gender.Men && ConfigUtils.GetUpdatedSetting("FavoriteMensTeam") != "empty")
            {
                string fifaCode = ConfigUtils.GetUpdatedSetting("FavoriteMensTeam");
                CbTeam.SelectedIndex = CbTeam.Items.IndexOf(teams.First(e => e.FifaCode == fifaCode));
            }
            else if (currentGender == IData.Gender.Women && ConfigUtils.GetUpdatedSetting("FavoriteWomensTeam") != "empty")
            {
                string fifaCode = ConfigUtils.GetUpdatedSetting("FavoriteWomensTeam");
                CbTeam.SelectedIndex = CbTeam.Items.IndexOf(teams.First(e => e.FifaCode == fifaCode));
            }
        }


        private void ClearInformation()
        {
            ScorePanel.Visibility = Visibility.Hidden;

            FootballField.Children.Clear();
        }

        private void LoadResolution()
        {
            try
            {
                var resolution = ConfigUtils.GetUpdatedSetting("Resolution");
                
                if (resolution == "Fullscreen")
                {
                    WindowState = WindowState.Maximized;
                } 
                else
                {
                    WindowState = WindowState.Normal;
                    var array = ConfigUtils.GetUpdatedSetting("Resolution").Split("x");

                    Height = Math.Min(SystemParameters.PrimaryScreenHeight - 30, int.Parse(array[1]));
                    Width = Math.Min(SystemParameters.PrimaryScreenWidth - 10, int.Parse(array[0]));
                }
            }
            catch
            {
                Height = 450;
                Width = 800;
            }
        }

        private void LoadGender()
        {
            try
            {
                currentGender = (IData.Gender)Enum.Parse(typeof(IData.Gender), ConfigUtils.GetUpdatedSetting("Priority"));
            }
            catch
            {
                currentGender = IData.Gender.Men;
            }
        }

        private void LoadCulture()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(ConfigUtils.GetUpdatedSetting("Language"));
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(ConfigUtils.GetUpdatedSetting("Language"));
            }
            catch
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(ConfigUtils.GetStandardSetting("Language"));
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(ConfigUtils.GetStandardSetting("Language"));
            }
        }

        private void CbTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ClearInformation();
                SaveCurrentTeam();
                InitializeLeftSide();
                LoadOpponentComboBox();
            }
            catch 
            {
                MessageBox.Show("Failed to display team.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveCurrentTeam()
        {
            var team = CbTeam.SelectedItem as Team;
            ConfigUtils.SetSetting(currentGender == IData.Gender.Men ? "FavoriteMensTeam" : "FavoriteWomensTeam", team.FifaCode);
            DataFactory.CreateConfiguration();
        }

        private async Task DisplayInfoAsync()
        {
            await LoadCurrentMatchAsync();

            ScorePanel.Visibility = Visibility.Visible;
            var team = CbTeam.SelectedItem as Team;
            var opponent = CbOpponentTeam.SelectedItem as Team;

            LabelHomeTeam.Content = team;
            LabelOpponentTeam.Content = opponent;

            if (currentMatch is not null)
            {
                if (currentMatch.HomeStatistics.Code == team.FifaCode)
                {
                    LabelHomeScore.Content = currentMatch.HomeStatistics.Goals;
                    LabelOpponentScore.Content = currentMatch.AwayStatistics.Goals;
                } 
                else
                {
                    LabelHomeScore.Content = currentMatch.AwayStatistics.Goals;
                    LabelOpponentScore.Content = currentMatch.HomeStatistics.Goals;
                }
            }
        }

        private async void LoadOpponentComboBox()
        {
            try
            {
                ChangeIndexEvent = false;
                CbOpponentTeam.Items.Clear();
                ChangeIndexEvent = true;

                Team currentTeam = CbTeam.SelectedItem as Team;

                if (currentTeam != null)
                {
                    List<Team> teams = await data.GetTeams(currentGender);
                    List<Match> matches = await data.GetMatches(currentTeam.FifaCode, currentGender);

                    foreach (var match in matches)
                    {
                        foreach (var team in teams)
                        {
                            if (team.FifaCode != currentTeam.FifaCode)
                            {
                                if (team.FifaCode == match.HomeStatistics.Code)
                                {
                                    CbOpponentTeam.Items.Add(team);
                                    break;
                                }
                                else if (team.FifaCode == match.AwayStatistics.Code)
                                {
                                    CbOpponentTeam.Items.Add(team);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch 
            {
                MessageBox.Show("Failed to display opponents.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private async void InitializeLeftSide()
        {
            try
            {
                var team = CbTeam.SelectedItem as Team;
                List<Player> players = await data.GetPlayersWithoutSubstitutes(team.FifaCode, currentGender);

                if (team is not null)
                {
                    // Instantiate all teams

                    List<Player> defenders = players.Where(p => p.Position == "Defender").ToList();
                    List<Player> midfielders = players.Where(p => p.Position == "Midfield").ToList();
                    List<Player> forwards = players.Where(p => p.Position == "Forward").ToList();

                    // Find max of rows and set
                    int rows = new List<string> { "Defender", "Midfield", "Forward" }
                                    .Select(position => players.Count(p => p.Position == position))
                                    .Max();
                    SetRows(rows);

                    // Instantiate goalie
                    CreatePlayerProfile(players.FirstOrDefault(p => p.Position == "Goalie"), rows / 2, 0, VerticalAlignment.Top);


                    SetPlayers(defenders, 1);
                    SetPlayers(midfielders, 2);
                    SetPlayers(forwards, 3);
                }
            }
            catch 
            {
                MessageBox.Show("Failed to instantiate left side of the Football Field.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SetPlayers(List<Player> players, int column)
        {
            for (int i = 0; i < players.Count; i++)
            {
                CreatePlayerProfile(players[i], i, column);
            }
        }

        private void SetRows(int rows)
        {
            FootballField.RowDefinitions.Clear();
            Grid grid = new Grid();
            for (int i = 0; i < rows; i++)
            {
                RowDefinition row = new RowDefinition();
                FootballField.RowDefinitions.Add(row);
            }
        }

        private void CreatePlayerProfile(Player player, int row, int column, VerticalAlignment alignment = VerticalAlignment.Center)
        {
            PlayerProfile playerProfile = new PlayerProfile(player);

            playerProfile.HorizontalAlignment = HorizontalAlignment.Center;
            playerProfile.VerticalAlignment = alignment;

            Grid.SetRow(playerProfile, row);
            Grid.SetColumn(playerProfile, column);

            FootballField.Children.Add(playerProfile);
        }

        private async Task LoadCurrentMatchAsync()
        {
            var team = CbTeam.SelectedItem as Team;
            List<Match> matches = await data.GetMatches(team.FifaCode, currentGender);

            var opponent = CbOpponentTeam.SelectedItem as Team;

            Match result = null;

            foreach (var match in matches)
            {
                if (match.AwayStatistics.Code == opponent.FifaCode)
                {
                    result = match;
                    break;
                }
                else if (match.HomeStatistics.Code == opponent.FifaCode)
                {
                    result = match;
                    break;
                }
            }

            currentMatch = result;
        }

        private void CbOpponentTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (!ChangeIndexEvent)
                {
                    return;
                }
                DisplayInfoAsync();
                InitializeRightSideAsync();
            }
            catch 
            {
                MessageBox.Show("Failed to display opponent team.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task InitializeRightSideAsync()
        {
            try
            {
                ClearColumns(new List<int> { 4, 5, 6, 7 });

                var team = CbOpponentTeam.SelectedItem as Team;
                List<Player> players = await data.GetPlayersWithoutSubstitutes(team.FifaCode, currentGender);

                if (team is not null)
                {
                    // Instantiate all teams

                    List<Player> defenders = players.Where(p => p.Position == "Defender").ToList();
                    List<Player> midfielders = players.Where(p => p.Position == "Midfield").ToList();
                    List<Player> forwards = players.Where(p => p.Position == "Forward").ToList();

                    // Find max of rows and set
                    int rows = new List<string> { "Defender", "Midfield", "Forward" }
                                    .Select(position => players.Count(p => p.Position == position))
                                    .Max();
                    SetRows(rows);

                    // Instantiate goalie
                    CreatePlayerProfile(players.FirstOrDefault(p => p.Position == "Goalie"), rows / 2, 7, VerticalAlignment.Top);


                    SetPlayers(defenders, 6);
                    SetPlayers(midfielders, 5);
                    SetPlayers(forwards, 4);
                }
            }
            catch
            {
                MessageBox.Show("Failed to instantiate right side of the Football Field.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearColumns(List<int> columns)
        {
            foreach (int column in columns)
            {
                var childrenToRemove = FootballField.Children
                    .OfType<PlayerProfile>()
                    .Where(x => Grid.GetColumn(x) == column)
                    .ToList();

                foreach (var child in childrenToRemove)
                {
                    FootballField.Children.Remove(child);
                }
            }
        }


        private void ItemConfig_Click(object sender, RoutedEventArgs e)
        {
            ConfigurationWindow form = new ConfigurationWindow(false);
            
            var formResult = form.ShowDialog();

            if (formResult != false)
            {
                var result = MessageBox.Show("To confirm these changes, you need to restart the application.", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Information);

                if (result == MessageBoxResult.OK)
                {
                    DataFactory.CreateConfiguration();
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
            }
        }

        private void LabelHomeTeam_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var team = CbTeam.SelectedItem as Team;
            TeamInformation info = new TeamInformation(team);

            Label label = sender as Label;
            DoubleAnimation animation = new DoubleAnimation
            {
                From = label.FontSize,
                To = label.FontSize * 1.2, 
                Duration = TimeSpan.FromSeconds(0.25), 
                AutoReverse = true
            };

            label.BeginAnimation(Label.FontSizeProperty, animation);

            info.ShowDialog();
        }

        private void LabelOpponentTeam_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var team = CbOpponentTeam.SelectedItem as Team;
            TeamInformation info = new TeamInformation(team);

            Label label = sender as Label;
            DoubleAnimation animation = new DoubleAnimation
            {
                From = label.FontSize,
                To = label.FontSize * 1.2,
                Duration = TimeSpan.FromSeconds(0.25),
                AutoReverse = true
            };

            label.BeginAnimation(Label.FontSizeProperty, animation);

            info.ShowDialog();
        }
    }
}
