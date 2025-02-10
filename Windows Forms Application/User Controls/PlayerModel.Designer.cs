namespace Windows_Forms_Application
{
    partial class PlayerModel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            picture = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            lblName = new Label();
            lblGoal = new Label();
            lblYellowCards = new Label();
            ((System.ComponentModel.ISupportInitialize)picture).BeginInit();
            SuspendLayout();
            // 
            // picture
            // 
            picture.Image = Properties.Resources.default_image;
            picture.Location = new Point(3, 3);
            picture.Name = "picture";
            picture.Size = new Size(113, 93);
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            picture.TabIndex = 0;
            picture.TabStop = false;
            picture.Click += picture_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(122, 12);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 1;
            label1.Text = "Name:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(122, 36);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 2;
            label2.Text = "Goals:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(122, 60);
            label3.Name = "label3";
            label3.Size = new Size(75, 15);
            label3.TabIndex = 3;
            label3.Text = "Yellow cards:";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(170, 12);
            lblName.Name = "lblName";
            lblName.Size = new Size(20, 15);
            lblName.TabIndex = 4;
            lblName.Text = "lbl";
            // 
            // lblGoal
            // 
            lblGoal.AutoSize = true;
            lblGoal.Location = new Point(170, 36);
            lblGoal.Name = "lblGoal";
            lblGoal.Size = new Size(20, 15);
            lblGoal.TabIndex = 5;
            lblGoal.Text = "lbl";
            // 
            // lblYellowCards
            // 
            lblYellowCards.AutoSize = true;
            lblYellowCards.Location = new Point(203, 60);
            lblYellowCards.Name = "lblYellowCards";
            lblYellowCards.Size = new Size(20, 15);
            lblYellowCards.TabIndex = 6;
            lblYellowCards.Text = "lbl";
            // 
            // PlayerModel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblYellowCards);
            Controls.Add(lblGoal);
            Controls.Add(lblName);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(picture);
            Name = "PlayerModel";
            Size = new Size(277, 101);
            Load += PlayerModel_Load;
            ((System.ComponentModel.ISupportInitialize)picture).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox picture;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label lblName;
        private Label lblGoal;
        private Label lblYellowCards;
    }
}
