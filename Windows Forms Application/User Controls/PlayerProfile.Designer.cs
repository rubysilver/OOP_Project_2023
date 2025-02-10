namespace Windows_Forms_Application
{
    partial class PlayerProfile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerProfile));
            pictureBox = new PictureBox();
            lblFullName = new Label();
            label1 = new Label();
            label2 = new Label();
            pictureBox1 = new PictureBox();
            label3 = new Label();
            lblShirtNumber = new Label();
            lblName = new Label();
            lblPosition = new Label();
            lblCaptain = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox
            // 
            resources.ApplyResources(pictureBox, "pictureBox");
            pictureBox.Cursor = Cursors.Hand;
            pictureBox.Image = Properties.Resources.default_image;
            pictureBox.Name = "pictureBox";
            pictureBox.TabStop = false;
            pictureBox.Click += pictureBox_Click;
            // 
            // lblFullName
            // 
            resources.ApplyResources(lblFullName, "lblFullName");
            lblFullName.Name = "lblFullName";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(pictureBox1, "pictureBox1");
            pictureBox1.Image = Properties.Resources.star;
            pictureBox1.Name = "pictureBox1";
            pictureBox1.TabStop = false;
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // lblShirtNumber
            // 
            resources.ApplyResources(lblShirtNumber, "lblShirtNumber");
            lblShirtNumber.Name = "lblShirtNumber";
            // 
            // lblName
            // 
            resources.ApplyResources(lblName, "lblName");
            lblName.Name = "lblName";
            // 
            // lblPosition
            // 
            resources.ApplyResources(lblPosition, "lblPosition");
            lblPosition.Name = "lblPosition";
            // 
            // lblCaptain
            // 
            resources.ApplyResources(lblCaptain, "lblCaptain");
            lblCaptain.Name = "lblCaptain";
            // 
            // PlayerProfile
            // 
            resources.ApplyResources(this, "$this");
            AllowDrop = true;
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblCaptain);
            Controls.Add(lblPosition);
            Controls.Add(lblName);
            Controls.Add(lblShirtNumber);
            Controls.Add(label3);
            Controls.Add(pictureBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblFullName);
            Controls.Add(pictureBox);
            PlayerName = "PlayerProfile";
            Load += PlayerProfile_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox;
        private Label lblFullName;
        private Label label1;
        private Label label2;
        public PictureBox pictureBox1;
        private Label label3;
        private Label lblShirtNumber;
        private Label lblName;
        private Label lblPosition;
        private Label lblCaptain;
    }
}
