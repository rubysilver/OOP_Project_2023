namespace Windows_Forms_Application
{
    partial class RankingForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RankingForm));
            btnPlayers = new Button();
            btnMatches = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnPlayers
            // 
            resources.ApplyResources(btnPlayers, "btnPlayers");
            btnPlayers.Name = "btnPlayers";
            btnPlayers.UseVisualStyleBackColor = true;
            btnPlayers.Click += btnPlayers_Click;
            // 
            // btnMatches
            // 
            resources.ApplyResources(btnMatches, "btnMatches");
            btnMatches.Name = "btnMatches";
            btnMatches.UseVisualStyleBackColor = true;
            btnMatches.Click += btnMatches_Click;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // RankingForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label1);
            Controls.Add(btnMatches);
            Controls.Add(btnPlayers);
            Name = "RankingForm";
            Load += RankingForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnPlayers;
        private Button btnMatches;
        private Label label1;
    }
}