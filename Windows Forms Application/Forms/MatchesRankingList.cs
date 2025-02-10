using Data_Layer.DAL;
using Data_Layer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows_Forms_Application.User_Controls;

namespace Windows_Forms_Application.Forms
{
    public partial class MatchesRankingList : Form
    {
        private IData data = DataFactory.GetData();
        private string fifaCode { get; set; }
        private IData.Gender gender { get; set; }
        private List<MatchModel> modelList = new List<MatchModel>();
        public MatchesRankingList(string fifaCode, IData.Gender gender)
        {
            InitializeComponent();
            this.fifaCode = fifaCode;
            this.gender = gender;
        }

        private async void MatchesRankingList_Load(object sender, EventArgs e)
        {
            try
            {
                FlpMatches.UseWaitCursor = true;
                this.CenterToParent();

                var matches = await data.GetMatches(fifaCode, gender);

                matches.Sort();

                foreach (var match in matches)
                {
                    modelList.Add(new MatchModel(match.HomeTeamCountry, match.AwayTeamCountry, match.Location, match.Attendance));
                }

                // Clear existing controls before adding sorted matches
                FlpMatches.Controls.Clear();

                foreach (var match in modelList)
                {
                    FlpMatches.Controls.Add(match);
                }

                PrintRanking();
            }
            catch 
            {
                MessageBox.Show("Failed to load matches", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                FlpMatches.UseWaitCursor = false;
            }
        }
        private void PrintRanking()
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocumentOnPrintPage;

            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = printDocument;

            printPreviewDialog.ShowDialog();
        }

        private void PrintDocumentOnPrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            float currentY = 20;

            // Print match results
            currentY += 20;
            graphics.DrawString("Match Results", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new PointF(20, currentY));
            currentY += 30;

            foreach (var match in modelList)
            {
                string homeTeam = match.HomeTeam;
                string awayTeam = match.AwayTeam;
                string location = match.LocationOfMatch;
                int attendance = match.Visitors;

                string matchInfo = $"{homeTeam} vs {awayTeam} at {location}, Attendance: {attendance}";
                graphics.DrawString(matchInfo, new Font("Arial", 12), Brushes.Black, new PointF(20, currentY));
                currentY += 20;
            }
        }
    }
}
