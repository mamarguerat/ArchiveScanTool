namespace ArchiveScanTool
{
    partial class archive_plans
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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mise en forme: [AFFAIRE] \\t [CARTON] \\r\\n";
            // 
            // richTextBox_plans
            // 
            this.richTextBox_plans.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_plans.Location = new System.Drawing.Point(13, 26);
            this.richTextBox_plans.Name = "richTextBox_plans";
            this.richTextBox_plans.Size = new System.Drawing.Size(775, 370);
            this.richTextBox_plans.TabIndex = 1;
            this.richTextBox_plans.Text = "";
            // 
            // btn_run
            // 
            this.btn_run.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_run.Location = new System.Drawing.Point(12, 415);
            this.btn_run.Name = "btn_run";
            this.btn_run.Size = new System.Drawing.Size(776, 23);
            this.btn_run.TabIndex = 2;
            this.btn_run.Text = "Démarer";
            this.btn_run.UseVisualStyleBackColor = true;
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
            // archive_plans
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.progressBar_progress);
            this.Controls.Add(this.btn_run);
            this.Controls.Add(this.richTextBox_plans);
            this.Controls.Add(this.label1);
            this.Name = "archive_plans";
            this.Text = "Mise des plans en archive";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox_plans;
        private System.Windows.Forms.Button btn_run;
        private System.Windows.Forms.ProgressBar progressBar_progress;
    }
}