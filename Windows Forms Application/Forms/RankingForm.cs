using Data_Layer.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows_Forms_Application.Forms;

namespace Windows_Forms_Application
{
    public partial class RankingForm : Form
    {
        private string fifaCode { get; set; }
        public IData.Gender gender { get; set; }
        public RankingForm(string fifaCode, IData.Gender gender)
        {
            InitializeComponent();
            this.gender = gender;
            this.fifaCode = fifaCode;
        }

        private void RankingForm_Load(object sender, EventArgs e)
        {
            InitializeConfiguration();
        }

        private void InitializeConfiguration()
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            this.CenterToScreen();
        }

        private void btnPlayers_Click(object sender, EventArgs e)
        {
            PlayerRankingList form = new PlayerRankingList(fifaCode, gender);

            form.ShowDialog();
        }

        private void btnMatches_Click(object sender, EventArgs e)
        {
            MatchesRankingList form = new MatchesRankingList(fifaCode, gender);

            form.ShowDialog();
        }
    }
}
