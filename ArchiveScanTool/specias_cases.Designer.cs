namespace ArchiveScanTool
{
    partial class specias_cases
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
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox_plans = new System.Windows.Forms.RichTextBox();
            this.btn_run = new System.Windows.Forms.Button();
            this.progressBar_progress = new System.Windows.Forms.ProgressBar();
            this.comboBox_mode = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(410, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mise en forme: [AFFAIRE] \\t [CARTON] \\r\\n (pas de carton pour les affaires détrui" +
    "tes)";
            // 
            // richTextBox_plans
            // 
            this.richTextBox_plans.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_plans.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.richTextBox_plans.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.richTextBox_plans.Location = new System.Drawing.Point(13, 33);
            this.richTextBox_plans.Name = "richTextBox_plans";
            this.richTextBox_plans.Size = new System.Drawing.Size(775, 363);
            this.richTextBox_plans.TabIndex = 1;
            this.richTextBox_plans.Text = "";
            // 
            // btn_run
            // 
            this.btn_run.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_run.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.btn_run.Location = new System.Drawing.Point(12, 415);
            this.btn_run.Name = "btn_run";
            this.btn_run.Size = new System.Drawing.Size(776, 23);
            this.btn_run.TabIndex = 2;
            this.btn_run.Text = "Démarer";
            this.btn_run.UseVisualStyleBackColor = false;
            this.btn_run.Click += new System.EventHandler(this.btn_run_Click);
            // 
            // progressBar_progress
            // 
            this.progressBar_progress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar_progress.Location = new System.Drawing.Point(15, 402);
            this.progressBar_progress.Name = "progressBar_progress";
            this.progressBar_progress.Size = new System.Drawing.Size(773, 10);
            this.progressBar_progress.TabIndex = 3;
            // 
            // comboBox_mode
            // 
            this.comboBox_mode.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.comboBox_mode.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.comboBox_mode.FormattingEnabled = true;
            this.comboBox_mode.Items.AddRange(new object[] {
            "Plans restants",
            "Affaires détruites",
            "Affaires reclassées"});
            this.comboBox_mode.Location = new System.Drawing.Point(639, 6);
            this.comboBox_mode.Name = "comboBox_mode";
            this.comboBox_mode.Size = new System.Drawing.Size(149, 21);
            this.comboBox_mode.TabIndex = 4;
            // 
            // specias_cases
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.comboBox_mode);
            this.Controls.Add(this.progressBar_progress);
            this.Controls.Add(this.btn_run);
            this.Controls.Add(this.richTextBox_plans);
            this.Controls.Add(this.label1);
            this.Name = "specias_cases";
            this.Text = "Cas spéciaux";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox_plans;
        private System.Windows.Forms.Button btn_run;
        private System.Windows.Forms.ProgressBar progressBar_progress;
        private System.Windows.Forms.ComboBox comboBox_mode;
    }
}