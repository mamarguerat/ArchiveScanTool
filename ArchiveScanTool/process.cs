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
                    //string newPath = @"\\TERMINAL\Data\Chantiers\" + folder[0] + "\\" + folder[1] + "\\" + folder[2] + "\\" + newFileName;
                    string newPath = path + "\\ok\\" + newFileName;
                    string errorPath = path + "\\error\\" + newFileName;
                    int temp;

                    //Check file
                    if (int.TryParse(folder[1], out temp) && int.TryParse(folder[2], out temp))
                    {
                        //Check folder
                        if (File.Exists(newPath))
                        {
                            //error
                            File.Move(file, errorPath);
                        }
                        else
                        {
                            string database;
                            //Control search in database
                            SearchDataBase(folder, out database);
                            if (database == "")
                            {
                                File.Move(file, errorPath);
                            }
                            else
                            {
                                //Update database
                                UpdateDataBase(folder, database);
                                //Check Path

                                //Move and rename
                                File.Move(file, newPath);
                            }
                        }
                    }
                }
            }
        }

        string[] ExtractName(string file, int fileNameLength)
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

        private string SearchDataBase(string[] folder, out string database)
        {
            //string connectionString = "Driver ={ Microsoft Access Driver(*.mdb, *.accdb)}; Dsn=" + database + ";";
            database = "rtec";
            string name = "";
            name = GetName(folder, database);
            if (name != "")
            {
                MessageBox.Show("Name: " + name, "Found in " + database);
                return name;
            }
            database = "nord";
            name = GetName(folder, database);
            if (name != "")
            {
                MessageBox.Show("Name: " + name, "Found in " + database);
                return name;
            }
            database = "";
            return name;
        }

        private string GetName(string[] folder, string database)
        {
            string connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\marti\OneDrive\Documents\GitHub\ArchiveScanTool\ArchiveScanTool\bin\Debug\" + database + "dt.accdb; Persist Security Info = False";
            string name = "";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                OleDbCommand cmd = con.CreateCommand();
                cmd.Connection = con;
                try
                {
                    con.Open();
                    string query = "select * from Cht where Cht_Numero='" + folder[0] + "." + folder[1] + "." + folder[2] + "'";
                    cmd.CommandText = query;

                    OleDbDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        name = rdr["Cht_Nom"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return name;
        }
    }
}
