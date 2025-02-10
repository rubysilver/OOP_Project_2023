namespace Windows_Forms_Application
{
    partial class ConfigurationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationForm));
            label1 = new Label();
            CbGender = new ComboBox();
            label2 = new Label();
            label3 = new Label();
            CbLanguage = new ComboBox();
            btnConfirm = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // CbGender
            // 
            resources.ApplyResources(CbGender, "CbGender");
            CbGender.DropDownStyle = ComboBoxStyle.DropDownList;
            CbGender.FormattingEnabled = true;
            CbGender.Name = "CbGender";
            CbGender.SelectedIndexChanged += CbGender_SelectedIndexChanged;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // CbLanguage
            // 
            resources.ApplyResources(CbLanguage, "CbLanguage");
            CbLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            CbLanguage.FormattingEnabled = true;
            CbLanguage.Name = "CbLanguage";
            CbLanguage.SelectedIndexChanged += CbLanguage_SelectedIndexChanged;
            // 
            // btnConfirm
            // 
            resources.ApplyResources(btnConfirm, "btnConfirm");
            btnConfirm.DialogResult = DialogResult.OK;
            btnConfirm.Name = "btnConfirm";
            btnConfirm.UseVisualStyleBackColor = true;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // btnCancel
            // 
            resources.ApplyResources(btnCancel, "btnCancel");
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Name = "btnCancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // ConfigurationForm
            // 
            AcceptButton = btnConfirm;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ControlBox = false;
            Controls.Add(btnCancel);
            Controls.Add(btnConfirm);
            Controls.Add(label3);
            Controls.Add(CbLanguage);
            Controls.Add(label2);
            Controls.Add(CbGender);
            Controls.Add(label1);
            Name = "ConfigurationForm";
            TopMost = true;
            Load += ConfigurationForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private ComboBox CbGender;
        private Label label2;
        private Label label3;
        private ComboBox CbLanguage;
        private Button btnConfirm;
        private Button btnCancel;
    }
}