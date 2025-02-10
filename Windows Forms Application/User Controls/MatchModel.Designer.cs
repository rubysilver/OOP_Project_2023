namespace Windows_Forms_Application.User_Controls
{
    partial class MatchModel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MatchModel));
            lblHomeTeam = new Label();
            lblAwayTeam = new Label();
            label3 = new Label();
            lblLocation = new Label();
            label1 = new Label();
            lblVisitors = new Label();
            SuspendLayout();
            // 
            // lblHomeTeam
            // 
            resources.ApplyResources(lblHomeTeam, "lblHomeTeam");
            lblHomeTeam.Name = "lblHomeTeam";
            // 
            // lblAwayTeam
            // 
            resources.ApplyResources(lblAwayTeam, "lblAwayTeam");
            lblAwayTeam.Name = "lblAwayTeam";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // lblLocation
            // 
            resources.ApplyResources(lblLocation, "lblLocation");
            lblLocation.Name = "lblLocation";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // lblVisitors
            // 
            resources.ApplyResources(lblVisitors, "lblVisitors");
            lblVisitors.Name = "lblVisitors";
            // 
            // MatchModel
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblVisitors);
            Controls.Add(label1);
            Controls.Add(lblLocation);
            Controls.Add(label3);
            Controls.Add(lblAwayTeam);
            Controls.Add(lblHomeTeam);
            Name = "MatchModel";
            Load += MatchModel_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblHomeTeam;
        private Label lblAwayTeam;
        private Label label3;
        private Label lblLocation;
        private Label label1;
        private Label lblVisitors;
    }
}
