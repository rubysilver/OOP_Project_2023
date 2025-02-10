using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Windows_Forms_Application.User_Controls
{
    public partial class MatchModel : UserControl, IComparable<MatchModel>
    {
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string LocationOfMatch { get; set; }
        public int Visitors { get; set; }

        public MatchModel(string homeTeam, string awayTeam, string location, int visitors)
        {
            InitializeComponent();
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            LocationOfMatch = location;
            Visitors = visitors;
        }

        private void MatchModel_Load(object sender, EventArgs e)
        {
            lblHomeTeam.Text = HomeTeam;
            lblAwayTeam.Text = AwayTeam;
            lblLocation.Text = LocationOfMatch;
            lblVisitors.Text = Visitors.ToString();
        }

        public override bool Equals(object? obj)
        {
            return obj is MatchModel model &&
                   HomeTeam == model.HomeTeam &&
                   AwayTeam == model.AwayTeam &&
                   LocationOfMatch == model.LocationOfMatch &&
                   Visitors == model.Visitors;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(HomeTeam, AwayTeam, LocationOfMatch, Visitors);
        }

        public int CompareTo(MatchModel? other)
        {
            return Visitors.CompareTo(other.Visitors);
        }
    }
}
