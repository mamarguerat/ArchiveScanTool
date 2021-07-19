using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace ArchiveScanTool
{
    public partial class specias_cases : Form
    {
        private string folderPath;
        private string nordPath;
        private string rtecPath;
        private string[] folder;

        public string Path
        {
            get { return folderPath; }
            set { folderPath = value; }
        }

        public string NordPath
        {
            get { return nordPath; }
            set { nordPath = value; }
        }
        public string RtecPath
        {
            get { return rtecPath; }
            set { rtecPath = value; }
        }

        public specias_cases()
        {
            InitializeComponent();
        }

        private void btn_run_Click(object sender, EventArgs e)
        {
            progressBar_progress.Maximum = richTextBox_plans.Lines.Count();
            if (comboBox_mode.SelectedItem.ToString() == "Plans restants")
            {
                foreach (string line in richTextBox_plans.Lines)
                {
                    string[] words = line.Split('\t');
                    string[] affaire = ExtractName(words[0]);
                    string path = GetPath(affaire);
                    string destination = folderPath + "\\" + path + "Archives scannées\\";
                    destination += "Plans non-numérisés dans boîte " + words[1] + ".txt";
                    using (StreamWriter file = new StreamWriter(destination))
                    {
                        file.WriteLine("ArchiveScanTool v1.2.4 - " + DateTime.Now.ToString("dd/MM/yyyy"));
                        file.WriteLine("──────────────────────────────────────────────────────────────");
                        file.WriteLine("Les plans plus grand que A3 n'ont pas pu être scannés. Ils se trouvent dans la boîte N°" + words[1]);
                    }
                    progressBar_progress.Value++;
                }
            }
            else if (comboBox_mode.SelectedItem.ToString() == "Affaires détruites")
            {
                foreach (string line in richTextBox_plans.Lines)
                {
                    UpdateDatabase(line, "détruit");
                    progressBar_progress.Value++;
                }
            }
            else if (comboBox_mode.SelectedItem.ToString() == "Affaires reclassées")
            {
                foreach (string line in richTextBox_plans.Lines)
                {
                    string[] words = line.Split('\t');
                    UpdateDatabase(words[0], words[1]);
                    progressBar_progress.Value++;
                }
            }
            if(progressBar_progress.Value == progressBar_progress.Maximum)
            {
                MessageBox.Show("Tout a été traité correctement", "Effectué", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UpdateDatabase(string line, string instruction)
        {
            string[] affaire = ExtractName(line);
            string connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = ";
            if (line.Substring(0, 1) == "N")
                connectionString += nordPath;
            else if (line.Substring(0, 1) == "R")
                connectionString += rtecPath;
            connectionString += "; Persist Security Info = False";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                OleDbCommand cmd = con.CreateCommand();
                cmd.Connection = con;
                try
                {
                    con.Open();
                    string query = "update Cht set CHT_BOITEINSTRUCTIONS='" + instruction + "' where Cht_Numero='" + line + "'";
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Extract the name from a string
        /// </summary>
        /// <param name="folderName">folder's name</param>
        /// <returns>Folder's in string array format</returns>
        private string[] ExtractName(string folderName)
        {
            folder = new string[4];
            int folderNameLength = folderName.Length;
            switch (folderNameLength)
            {
                case 12:
                    folder[0] = folderName.Substring(0, 2);
                    folder[1] = folderName.Substring(3, 4);
                    folder[2] = folderName.Substring(8, 3);
                    folder[3] = folderName.Substring(11, 1);
                    break;
                case 11:
                    folder[0] = folderName.Substring(0, 2);
                    folder[1] = folderName.Substring(3, 4);
                    folder[2] = folderName.Substring(8, 3);
                    folder[3] = "";
                    break;
                default:
                    folder[0] = "00";
                    folder[1] = "0000";
                    folder[2] = "000";
                    folder[3] = "";
                    break;
            }
            folder[0] = folder[0].ToUpper();
            folder[3] = folder[3].ToUpper();
            return folder;
        }

        /// <summary>
        /// Return path for the file
        /// </summary>
        /// <returns>file's path</returns>
        public string GetPath(string[] folder)
        {
            return folder[0] + "\\" + folder[1] + "\\" + folder[2] + "\\";
        }
    }
}
