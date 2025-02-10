using Data_Layer.DAL;
using Data_Layer.Models;
using Data_Layer.Utilities;
using Data_Layer.Utilities.Windows_Forms;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq.Expressions;

namespace Windows_Forms_Application
{
    public partial class GeneralForm : Form
    {
        private IData data;
        private IData.Gender currentGender;
        private List<PlayerProfile> selectedPlayers = new List<PlayerProfile>();
        private bool allowChangeIndexEvent = true;
        private static int currentIndexSelected = 0;
        private string currentFifaCode;
        public GeneralForm()
        {
            // If I don't initialize components, I don't have a reference to them.
            InitializeComponent();
        }
        private void GeneralForm_Load(object sender, EventArgs e)
        {
            try
            {
                ShowConfigurationForm();
                LoadRepository();
                InitializeConfiguration();
                InitializeTeamComboBox();
            }
            catch
            {
                MessageBox.Show($"Failed to load repository {ConfigUtils.GetStandardSetting("Repository")}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Dispose();
            }
        }
        // Methods for configuration
        private void ShowConfigurationForm()
        {
            if (File.Exists(ConfigUtils.filePath))
            {
                return;
            }

            ConfigurationForm configuration = new ConfigurationForm(true);
            configuration.ShowDialog();
        }
        private void LoadRepository()
        {
            data = DataFactory.GetData();
        }
        private void InitializeConfiguration()
        {
            Controls.Clear();
            SetNewCulture();
            InitializeComponent();
            InitializeContainerEvents();
            this.CenterToScreen();

            try
            {
                currentGender = (IData.Gender)Enum.Parse(typeof(IData.Gender), ConfigUtils.GetUpdatedSetting("Priority"));
            }
            catch
            {
                currentGender = IData.Gender.Men;
            }
        }

        private void InitializeContainerEvents()
        {
            flpPlayers.DragEnter += FlowLayoutPanel_DragEnter;
            flpPlayers.DragDrop += FlowLayoutPanel_DragDrop;

            flpFavoritePlayers.DragEnter += FlowLayoutPanel_DragEnter;
            flpFavoritePlayers.DragDrop += FlowLayoutPanel_DragDrop;
        }

        private async void InitializeTeamComboBox()
        {
            try
            {
                flpPlayers.UseWaitCursor = true;
                flpFavoritePlayers.UseWaitCursor = true;
                List<Team> teams = await data.GetTeams(currentGender);
                foreach (var team in teams)
                {
                    CbTeams.Items.Add(team);
                }

                if (currentGender == IData.Gender.Men && ConfigUtils.GetUpdatedSetting("FavoriteMensTeam") != "empty")
                {
                    string fifaCode = ConfigUtils.GetUpdatedSetting("FavoriteMensTeam");
                    CbTeams.SelectedIndex = CbTeams.Items.IndexOf(teams.First(e => e.FifaCode == fifaCode));
                }
                else if (currentGender == IData.Gender.Women && ConfigUtils.GetUpdatedSetting("FavoriteWomensTeam") != "empty")
                {
                    string fifaCode = ConfigUtils.GetUpdatedSetting("FavoriteWomensTeam");
                    CbTeams.SelectedIndex = CbTeams.Items.IndexOf(teams.First(e => e.FifaCode == fifaCode));
                }
                else
                {
                    CbTeams.SelectedIndex = currentIndexSelected > CbTeams.Controls.Count ? 0 : currentIndexSelected;
                }
            }
            catch
            {
                MessageBox.Show("Failed to load team.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            flpPlayers.UseWaitCursor = false;
            flpFavoritePlayers.UseWaitCursor = false;
        }
        private void SetNewCulture()
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
        // Events
        private void settingToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var current = new Culture(Thread.CurrentThread.CurrentCulture.NativeName, CultureInfo.GetCultureInfo(Thread.CurrentThread.CurrentCulture.Name));
            ConfigurationForm configurationForm = new ConfigurationForm(false, currentGender, current);

            if (configurationForm.ShowDialog() == DialogResult.OK)
            {
                // Clear controls
                Controls.Clear();
                // Recreate file
                DataFactory.CreateConfiguration();
                // Update enum gender
                currentGender = (IData.Gender)Enum.Parse(typeof(IData.Gender), ConfigUtils.GetUpdatedSetting("Priority"));
                // Update thread to new culture
                SetNewCulture();
                // Reinitialize all components
                InitializeComponent();
                // Reinitialize container events
                InitializeContainerEvents();
                // Repopulate flow layout panel
                InitializeTeamComboBox();
            }

        }
        private async void CbTeams_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                flpPlayers.UseWaitCursor = true;
                flpFavoritePlayers.UseWaitCursor = true;
                if (!allowChangeIndexEvent)
                {
                    return;
                }

                if (flpFavoritePlayers.Controls.Count > 0
                    && MessageBox.Show("Are you sure you want to change teams while you have a active selection in your favorite players?", "Notification",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)
                    != DialogResult.Yes)
                {
                    allowChangeIndexEvent = false;
                    CbTeams.SelectedIndex = currentIndexSelected;
                    allowChangeIndexEvent = true;
                    return;
                }

                ClearAllContainers();
                if (currentGender == IData.Gender.Men && ConfigUtils.GetUpdatedSetting("FavoriteMensPlayers") == "empty")
                {
                    UpdateFavoritePlayersConfig();
                }
                else if (currentGender == IData.Gender.Women && ConfigUtils.GetUpdatedSetting("FavoriteWomensPlayers") == "empty")
                {
                    UpdateFavoritePlayersConfig();
                }


                var team = CbTeams.SelectedItem as Team;
                currentIndexSelected = CbTeams.SelectedIndex;
                currentFifaCode = team.FifaCode;

                ConfigUtils.SetSetting(currentGender == IData.Gender.Men ? "FavoriteMensTeam" : "FavoriteWomensTeam", team.FifaCode);

                if (team != null)
                {
                    List<Player> players = await data.GetPlayers(team.FifaCode, currentGender);

                    string savedPlayers = ConfigUtils.GetUpdatedSetting(currentGender == IData.Gender.Men ? "FavoriteMensPlayers" : "FavoriteWomensPlayers");
                    List<string> listOfSavedPlayers = new List<string>();
                    if (savedPlayers != "empty")
                    {
                        listOfSavedPlayers = savedPlayers.Split(',').ToList();

                        SavePlayersToSettings("FavoriteMensPlayers");
                        SavePlayersToSettings("FavoriteWomensPlayers");
                    }

                    foreach (var player in players)
                    {
                        var profile = new PlayerProfile(player.Id, player.Name, player.Position, player.Captain, CmsPlayers);
                        if (listOfSavedPlayers.Count > 0)
                        {
                            if (listOfSavedPlayers.Any(e => e == player.Name))
                            {
                                flpFavoritePlayers.Controls.Add(profile);
                                selectedPlayers.Add(profile);
                                profile.pictureBox1.Visible = true;
                            }
                            else
                            {
                                flpPlayers.Controls.Add(profile);
                            }
                        }
                        else
                        {
                            flpPlayers.Controls.Add(profile);
                        }
                        profile.MouseDown += PlayerProfile_MouseDown;
                    }
                }

                DataFactory.CreateConfiguration();
            }
            catch
            {
                MessageBox.Show("Failed to load team.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            flpPlayers.UseWaitCursor = false;
            flpFavoritePlayers.UseWaitCursor = false;
        }

        private void PlayerProfile_MouseDown(object sender, MouseEventArgs e)
        {
            var control = sender as PlayerProfile;
            if (control != null)
            {
                control.DoDragDrop(control, DragDropEffects.Move);
            }
        }

        private void FlowLayoutPanel_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void FlowLayoutPanel_DragDrop(object sender, DragEventArgs e)
        {
            var control = e.Data.GetData(typeof(PlayerProfile)) as PlayerProfile;
            var destination = sender as FlowLayoutPanel;

            if (control != null && destination != null)
            {
                if (destination.Name == flpPlayers.Name)
                {
                    control.Parent = destination;
                    selectedPlayers.Remove(control);
                    control.pictureBox1.Visible = false;
                }
                else if (selectedPlayers.Count != 3)
                {
                    control.Parent = destination;
                    selectedPlayers.Add(control);
                    control.pictureBox1.Visible = true;
                }
                UpdateFavoritePlayersConfig();
            }
        }

        private void SavePlayersToSettings(string property)
        {
            try
            {
                string players = ConfigUtils.GetUpdatedSetting(property);

                if (property != "empty")
                {
                    ConfigUtils.SetSetting(property, players);
                }
            }
            catch
            {
                MessageBox.Show("Failed to save players to disk.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearAllContainers()
        {
            selectedPlayers.Clear();
            flpFavoritePlayers.Controls.Clear();
            flpPlayers.Controls.Clear();
        }
        private void OcmsChangePanel_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item && item.Owner is ContextMenuStrip menu)
            {
                var player = menu.SourceControl as PlayerProfile;
                if (player.Parent.Name == flpPlayers.Name)
                {
                    if (selectedPlayers.Count > 2)
                    {
                        return;
                    }

                    // User interface
                    player.Parent = flpFavoritePlayers;
                    player.pictureBox1.Visible = true;

                    // List of selected players
                    selectedPlayers.Add(player);

                    // Update text file
                    UpdateFavoritePlayersConfig();
                }
                else
                {
                    player.Parent = flpPlayers;
                    player.pictureBox1.Visible = false;

                    selectedPlayers.Remove(player);

                    UpdateFavoritePlayersConfig();
                }
            }
        }
        private void UpdateFavoritePlayersConfig()
        {
            string players = string.Join(',', selectedPlayers);
            ConfigUtils.SetSetting(currentGender is IData.Gender.Men ? "FavoriteMensPlayers" : "FavoriteWomensPlayers", players == "" ? "empty" : players);
            DataFactory.CreateConfiguration();
        }

        private void GeneralForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.Cancel != true)
            {
                DialogResult dialog = MessageBox.Show("Are you sure you want to exit the application?", "Notifcation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    Environment.Exit(0);
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void BtnRanking_Click(object sender, EventArgs e)
        {
            RankingForm form = new RankingForm(currentFifaCode, currentGender);

            form.Show();
        }
    }
}