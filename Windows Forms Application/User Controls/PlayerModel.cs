using Data_Layer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Windows_Forms_Application
{
    public partial class PlayerModel : UserControl, IComparable<PlayerModel>
    {
        public string NamePlayer { get; set; }
        public int Goals { get; set; }
        public int YellowCards { get; set; }
        public PlayerModel(string name, int goals = 0, int yellowCards = 0)
        {
            InitializeComponent();
            NamePlayer = name;
            Goals = goals;
            YellowCards = yellowCards;
        }

        private void PlayerModel_Load(object sender, EventArgs e)
        {
            lblName.Text = NamePlayer;
            lblGoal.Text = Goals.ToString();
            lblYellowCards.Text = YellowCards.ToString();
        }

        private void picture_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog()
            {
                Filter = "Pictures|*.jpeg;*.jpg;*.png;",
                Multiselect = false,
                Title = $"Choose profile picture for {Name}",
                InitialDirectory = Application.StartupPath
            };

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                picture.Image = Image.FromFile(fileDialog.FileName);
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is PlayerModel model &&
                   Goals == model.Goals &&
                   YellowCards == model.YellowCards;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Goals, YellowCards);
        }

        public int CompareTo(PlayerModel? other)
        {
            // Sort by number of goals (descending order)
            int goalsComparison = other.Goals.CompareTo(Goals);
            if (goalsComparison != 0)
            {
                return goalsComparison;
            }

            // Sort by number of yellow cards (ascending order)
            return YellowCards.CompareTo(other.YellowCards);
        }
    }
}

