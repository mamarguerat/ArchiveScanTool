using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Xml;

namespace ArchiveScanTool
{
    class process
    {
        public void ProcessFile()
        {
            string path = Directory.GetCurrentDirectory();
            string[] files = Directory.GetFiles(path, "*.pdf");
            foreach (string file in files)
            {
                //Extract name
                string fileName = file.Substring(path.Length + 1);
                int fileNameLength = fileName.Length;
                string[] folder = ExtractName(fileName, fileNameLength);
                if (folder[0] != "00")
                {
                    string newFileName = folder[0] + "." + folder[1] + "." + folder[2] + ".pdf";
                    //string newPath = "\\TERMINAL\\Data\\Chantiers\\" + folder[0] + "\\" + folder[1] + "\\" + folder[2] + "\\" + newFileName;
                    string newPath = path + "\\" + newFileName;
                    int temp;

                    //Check file
                    if (int.TryParse(folder[1], out temp) && int.TryParse(folder[2], out temp))
                    {
                        //if (File.Exists(newPath))
                        //{
                        //    //error
                        //}
                        //else
                        //{
                            //Check folder

                            //Update database
                            //UpdateDataBase(folder, "nord");
                            SearchDataBase(folder, "nord");
                        //Move and rename
                        //    File.Move(file, newPath);
                        //}
                    }
                }
            }
        }

        private string[] ExtractName(string file, int fileNameLength)
        {
            string[] folder = new string[3];

            switch (fileNameLength)
            {
                case 15:
                    folder[0] = file.Substring(0, 2);
                    folder[1] = file.Substring(3, 4);
                    folder[2] = file.Substring(8, 3);
                    break;
                case 12:
                    folder[0] = file.Substring(0, 2);
                    folder[1] = file.Substring(2, 4);
                    folder[2] = file.Substring(6, 3);
                    break;
                default:
                    folder[0] = "00";
                    folder[1] = "0000";
                    folder[2] = "000";
                    break;
            }
            folder[0] = folder[0].ToUpper();
            return folder;
        }

        private void UpdateDataBase(string[] folder, string database)
        {
            //string connectionString = "Driver ={ Microsoft Access Driver(*.mdb, *.accdb)}; Dsn=" + database + ";";
            string connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\marti\OneDrive\Documents\GitHub\ArchiveScanTool\ArchiveScanTool\bin\Debug\rtecdt.accdb; Persist Security Info = False";
            try
            {
                OleDbConnection con = new OleDbConnection(connectionString);
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.Connection = con;
                string query = "update Cht set Cht_Dossier='num.' ,CHT_BOITEINSTRUCTIONS='num.' where Cht_Numero='" + folder[0] + "." + folder[1] + "." + folder[2] + "'";
                cmd.CommandText = query;

                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Submitted", "Congrats");
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchDataBase(string[] folder, string database)
        {
            //string connectionString = "Driver ={ Microsoft Access Driver(*.mdb, *.accdb)}; Dsn=" + database + ";";
            string connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\marti\OneDrive\Documents\GitHub\ArchiveScanTool\ArchiveScanTool\bin\Debug\rtecdt.accdb; Persist Security Info = False";
            try
            {
                OleDbConnection con = new OleDbConnection(connectionString);
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.Connection = con;
                string query = "select * from Cht where Cht_Numero='" + folder[0] + "." + folder[1] + "." + folder[2] + "'";
                cmd.CommandText = query;

                OleDbDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    MessageBox.Show("Name: " + rdr["Cht_Nom"].ToString(), "Congrats");
                }
                MessageBox.Show("Record Submitted", "Congrats");
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
