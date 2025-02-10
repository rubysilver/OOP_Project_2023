using Data_Layer.DAL;
using Data_Layer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Formats.Asn1.AsnWriter;

namespace Windows_Forms_Application
{
    public partial class PlayerRankingList : Form
    {
        private IData data = DataFactory.GetData();
        private string fifaCode { get; set; }
        private IData.Gender gender { get; set; }
        private List<PlayerModel> sortedModels;
        public PlayerRankingList(string fifaCode, IData.Gender gender)
        {
            InitializeComponent();
            this.fifaCode = fifaCode;
            this.gender = gender;
        }

        private async void PlayerRankingList_Load(object sender, EventArgs e)
        {
            try
            {
                flpPlayers.UseWaitCursor = true;
                this.CenterToParent();

                List<Player> players = await data.GetPlayers(fifaCode, gender);

                List<PlayerModel> models = await InitializePlayerModeListAsync(players);

                sortedModels = models.OrderBy(model => -model.Goals).ThenBy(model => model.YellowCards).ToList();

                flpPlayers.Controls.Clear();

                foreach (var model in sortedModels)
                {
                    flpPlayers.Controls.Add(model);
                }

                PrintRanking();
            }
            catch
            {
                MessageBox.Show("Failed to load players.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                flpPlayers.UseWaitCursor = false;
            }
        }


        private async Task<List<PlayerModel>> InitializePlayerModeListAsync(List<Player> players)
        {
            List<PlayerModel> models = new List<PlayerModel>();
            foreach (var player in players)
            {
                models.Add(new PlayerModel(player.Name, await GetGoalsAsync(player), await GetYellowCardsAsync(player)));
            }
            return models;
        }

        private async Task<int> GetGoalsAsync(Player player)
        {
            List<Match> matches = await data.GetMatches(fifaCode, gender);
            string[] goalEvent = { "goal", "own-goal", "goal-penalty" };
            int score = 0;

            foreach (Match match in matches)
            {
                foreach (var homeTeamEvent in match.HomeTeamEvents)
                {
                    var home = homeTeamEvent.TypeOfEvent.ToLower() ?? "";
                    if (homeTeamEvent.Player == player.Name && goalEvent.Contains(homeTeamEvent.TypeOfEvent))
                    {
                        score++;
                    }
                }


                foreach (var awayTeamEvents in match.AwayTeamEvents)
                {
                    var home = awayTeamEvents.TypeOfEvent.ToLower() ?? "";
                    if (awayTeamEvents.Player == player.Name && goalEvent.Contains(awayTeamEvents.TypeOfEvent))
                    {
                        score++;
                    }
                }

            }
            return score;
        }

        private async Task<int> GetYellowCardsAsync(Player player)
        {
            List<Match> matches = await data.GetMatches(fifaCode, gender);
            string[] goalEvent = { "yellow-card", "second-yellow-card" };
            int score = 0;

            foreach (Match match in matches)
            {
                foreach (var homeTeamEvent in match.HomeTeamEvents)
                {
                    var home = homeTeamEvent.TypeOfEvent.ToLower() ?? "";
                    if (homeTeamEvent.Player == player.Name && goalEvent.Contains(homeTeamEvent.TypeOfEvent))
                    {
                        score++;
                    }
                }

                foreach (var awayTeamEvent in match.AwayTeamEvents)
                {
                    var home = awayTeamEvent.TypeOfEvent.ToLower() ?? "";
                    if (awayTeamEvent.Player == player.Name && goalEvent.Contains(awayTeamEvent.TypeOfEvent))
                    {
                        score++;
                    }
                }

            }
            return score;
        }

        private void PrintRanking()
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocumentOnPrintPage;

            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog()
            {
                Document = printDocument
            };

            printPreviewDialog.ShowDialog();
        }

        private void PrintDocumentOnPrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            float currentY = 20;

            // Print player ranking
            graphics.DrawString("Player Ranking", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new PointF(20, currentY));
            currentY += 30;

            foreach (var model in sortedModels)
            {
                string playerName = model.NamePlayer;
                int playerScore = model.Goals;
                int yellowCards = model.YellowCards;

                string playerInfo = $"{playerName}: {playerScore} points, {yellowCards} yellow cards";
                graphics.DrawString(playerInfo, new Font("Arial", 12), Brushes.Black, new PointF(20, currentY));
                currentY += 20;
            }

        }
    }
}
