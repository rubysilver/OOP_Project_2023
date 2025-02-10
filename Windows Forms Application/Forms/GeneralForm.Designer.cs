namespace Windows_Forms_Application
{
    partial class GeneralForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneralForm));
            label3 = new Label();
            label2 = new Label();
            flpFavoritePlayers = new FlowLayoutPanel();
            flpPlayers = new FlowLayoutPanel();
            label1 = new Label();
            CbTeams = new ComboBox();
            menuStrip1 = new MenuStrip();
            MenuSetting = new ToolStripMenuItem();
            settingToolStripMenuItem = new ToolStripMenuItem();
            CmsPlayers = new ContextMenuStrip(components);
            OcmsChangePanel = new ToolStripMenuItem();
            BtnRanking = new Button();
            menuStrip1.SuspendLayout();
            CmsPlayers.SuspendLayout();
            SuspendLayout();
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // flpFavoritePlayers
            // 
            resources.ApplyResources(flpFavoritePlayers, "flpFavoritePlayers");
            flpFavoritePlayers.AllowDrop = true;
            flpFavoritePlayers.BackColor = SystemColors.ButtonFace;
            flpFavoritePlayers.BorderStyle = BorderStyle.FixedSingle;
            flpFavoritePlayers.Name = "flpFavoritePlayers";
            // 
            // flpPlayers
            // 
            resources.ApplyResources(flpPlayers, "flpPlayers");
            flpPlayers.AllowDrop = true;
            flpPlayers.BackColor = SystemColors.ButtonFace;
            flpPlayers.BorderStyle = BorderStyle.FixedSingle;
            flpPlayers.Name = "flpPlayers";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // CbTeams
            // 
            resources.ApplyResources(CbTeams, "CbTeams");
            CbTeams.DropDownStyle = ComboBoxStyle.DropDownList;
            CbTeams.FormattingEnabled = true;
            CbTeams.Name = "CbTeams";
            CbTeams.SelectedIndexChanged += CbTeams_SelectedIndexChanged;
            // 
            // menuStrip1
            // 
            resources.ApplyResources(menuStrip1, "menuStrip1");
            menuStrip1.Items.AddRange(new ToolStripItem[] { MenuSetting });
            menuStrip1.Name = "menuStrip1";
            // 
            // MenuSetting
            // 
            resources.ApplyResources(MenuSetting, "MenuSetting");
            MenuSetting.DropDownItems.AddRange(new ToolStripItem[] { settingToolStripMenuItem });
            MenuSetting.Name = "MenuSetting";
            // 
            // settingToolStripMenuItem
            // 
            resources.ApplyResources(settingToolStripMenuItem, "settingToolStripMenuItem");
            settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            settingToolStripMenuItem.Click += settingToolStripMenuItem_Click_1;
            // 
            // CmsPlayers
            // 
            resources.ApplyResources(CmsPlayers, "CmsPlayers");
            CmsPlayers.Items.AddRange(new ToolStripItem[] { OcmsChangePanel });
            CmsPlayers.Name = "CmsPlayers";
            // 
            // OcmsChangePanel
            // 
            resources.ApplyResources(OcmsChangePanel, "OcmsChangePanel");
            OcmsChangePanel.Name = "OcmsChangePanel";
            OcmsChangePanel.Click += OcmsChangePanel_Click;
            // 
            // BtnRanking
            // 
            resources.ApplyResources(BtnRanking, "BtnRanking");
            BtnRanking.Name = "BtnRanking";
            BtnRanking.UseVisualStyleBackColor = true;
            BtnRanking.Click += BtnRanking_Click;
            // 
            // GeneralForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(BtnRanking);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(flpFavoritePlayers);
            Controls.Add(flpPlayers);
            Controls.Add(label1);
            Controls.Add(CbTeams);
            Controls.Add(menuStrip1);
            Name = "GeneralForm";
            FormClosing += GeneralForm_FormClosing;
            Load += GeneralForm_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            CmsPlayers.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label3;
        private Label label2;
        private FlowLayoutPanel flpFavoritePlayers;
        private FlowLayoutPanel flpPlayers;
        private Label label1;
        private ComboBox CbTeams;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem MenuSetting;
        private ToolStripMenuItem settingToolStripMenuItem;
        private ContextMenuStrip CmsPlayers;
        private ToolStripMenuItem OcmsChangePanel;
        private Button BtnRanking;
    }
}