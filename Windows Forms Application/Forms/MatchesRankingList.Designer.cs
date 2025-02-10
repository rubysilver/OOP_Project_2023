namespace Windows_Forms_Application.Forms
{
    partial class MatchesRankingList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MatchesRankingList));
            FlpMatches = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // FlpMatches
            // 
            resources.ApplyResources(FlpMatches, "FlpMatches");
            FlpMatches.Name = "FlpMatches";
            // 
            // MatchesRankingList
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(FlpMatches);
            Name = "MatchesRankingList";
            Load += MatchesRankingList_Load;
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel FlpMatches;
    }
}