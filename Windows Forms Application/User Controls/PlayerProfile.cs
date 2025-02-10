using Data_Layer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Windows_Forms_Application
{
    public partial class PlayerProfile : UserControl
    {
        public long Id { get; set; }
        public string PlayerName { get; set; }
        public string Position { get; set; }
        public bool Captain { get; set; }
        public PlayerProfile(long shirtNumber, string fullName, string position, bool captain, ContextMenuStrip? menustrip)
        {
            InitializeComponent();
            Id = shirtNumber;
            PlayerName = fullName;
            Position = position;
            Captain = captain;
            if (menustrip is not null)
            {
                this.ContextMenuStrip = menustrip;
            }
        }

        private void PlayerProfile_Load(object sender, EventArgs e)
        {
            lblShirtNumber.Text = Id.ToString();
            lblName.Text = PlayerName;
            lblPosition.Text = Position;
            lblCaptain.Text = Captain is true ? "Yes" : "No";
            pictureBox1.Visible = false;
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog()
            {
                Filter = "Pictures|*.jpeg;*.jpg;*.png;",
                Multiselect = false,
                Title = $"Choose profile picture for {PlayerName}",
                InitialDirectory = Application.StartupPath
            };

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox.Image = Image.FromFile(fileDialog.FileName);
            }
        }

        public override string ToString() => $"{PlayerName}";
    }
}
