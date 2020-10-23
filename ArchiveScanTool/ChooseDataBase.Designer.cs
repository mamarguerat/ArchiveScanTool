namespace ArchiveScanTool
{
    partial class ChooseDataBase
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
            this.buttonBrowseNord = new System.Windows.Forms.Button();
            this.textBoxPathNord = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonBrowseTec = new System.Windows.Forms.Button();
            this.textBoxPathRtec = new System.Windows.Forms.TextBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonAnnuler = new System.Windows.Forms.Button();
            this.openFileDialogPath = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // buttonBrowseNord
            // 
            this.buttonBrowseNord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseNord.Location = new System.Drawing.Point(555, 25);
            this.buttonBrowseNord.Name = "buttonBrowseNord";
            this.buttonBrowseNord.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowseNord.TabIndex = 5;
            this.buttonBrowseNord.Text = "Parcourir...";
            this.buttonBrowseNord.UseVisualStyleBackColor = true;
            this.buttonBrowseNord.Click += new System.EventHandler(this.buttonBrowseNord_Click);
            // 
            // textBoxPathNord
            // 
            this.textBoxPathNord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPathNord.Location = new System.Drawing.Point(12, 27);
            this.textBoxPathNord.Name = "textBoxPathNord";
            this.textBoxPathNord.Size = new System.Drawing.Size(537, 20);
            this.textBoxPathNord.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Base de données NordVent RTech";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Base de données NordVent";
            // 
            // buttonBrowseTec
            // 
            this.buttonBrowseTec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseTec.Location = new System.Drawing.Point(555, 80);
            this.buttonBrowseTec.Name = "buttonBrowseTec";
            this.buttonBrowseTec.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowseTec.TabIndex = 9;
            this.buttonBrowseTec.Text = "Parcourir...";
            this.buttonBrowseTec.UseVisualStyleBackColor = true;
            this.buttonBrowseTec.Click += new System.EventHandler(this.buttonBrowseTec_Click);
            // 
            // textBoxPathRtec
            // 
            this.textBoxPathRtec.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPathRtec.Location = new System.Drawing.Point(12, 82);
            this.textBoxPathRtec.Name = "textBoxPathRtec";
            this.textBoxPathRtec.Size = new System.Drawing.Size(537, 20);
            this.textBoxPathRtec.TabIndex = 8;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Location = new System.Drawing.Point(474, 126);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 10;
            this.buttonOk.Text = "&OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonAnnuler
            // 
            this.buttonAnnuler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonAnnuler.Location = new System.Drawing.Point(555, 126);
            this.buttonAnnuler.Name = "buttonAnnuler";
            this.buttonAnnuler.Size = new System.Drawing.Size(75, 23);
            this.buttonAnnuler.TabIndex = 11;
            this.buttonAnnuler.Text = "&Annuler";
            this.buttonAnnuler.UseVisualStyleBackColor = true;
            // 
            // openFileDialogPath
            // 
            this.openFileDialogPath.CheckFileExists = false;
            this.openFileDialogPath.FileName = "Choisir un dossier";
            this.openFileDialogPath.Filter = "Base de donnée Access (*.accdb) | *.accdb";
            this.openFileDialogPath.RestoreDirectory = true;
            this.openFileDialogPath.ValidateNames = false;
            // 
            // ChooseDataBase
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonAnnuler;
            this.ClientSize = new System.Drawing.Size(642, 161);
            this.Controls.Add(this.buttonAnnuler);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonBrowseTec);
            this.Controls.Add(this.textBoxPathRtec);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonBrowseNord);
            this.Controls.Add(this.textBoxPathNord);
            this.MaximumSize = new System.Drawing.Size(700, 200);
            this.MinimumSize = new System.Drawing.Size(350, 200);
            this.Name = "ChooseDataBase";
            this.Text = "Choix des emplacements de bases de données";
            this.Load += new System.EventHandler(this.ChooseDataBase_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonBrowseNord;
        private System.Windows.Forms.TextBox textBoxPathNord;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonBrowseTec;
        private System.Windows.Forms.TextBox textBoxPathRtec;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonAnnuler;
        private System.Windows.Forms.OpenFileDialog openFileDialogPath;
    }
}