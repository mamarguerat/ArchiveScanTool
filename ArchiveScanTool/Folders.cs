using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Windows.Forms;

namespace ArchiveScanTool
{
    class Folders
    {
        private string file;
        private string[] folder;
        private string fileType;
        private string name;
        private string database;
        private string nordPath;
        private string rtecPath;

        public string File
        {
            get { return file; }
        }
        public string FileType
        {
            get { return fileType; }
            set { fileType = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Database
        {
            get { return database; }
        }
        public string NordPath
        {
            set { nordPath = value; }
        }
        public string RtecPath
        {
            set { rtecPath = value; }
        }

        public Folders(string fileName, string nord, string rtec)
        {
            file = fileName;
            nordPath = nord;
            rtecPath = rtec;
            folder = new string[3];
            Update();
        }

        public void Update()
        {
            if (fileType == null)
            {
                fileType = "Général";
            }
            SearchDataBase();
        }

        public void FolderName(string folderName)
        {
            folder = ExtractName(folderName);
            Update();
        }

        public void FolderName(string[] folderName)
        {
            folder = folderName;
            Update();
        }

        public string GetFolderNr()
        {
            if (folder[3] != null && folder[3] != "")
                return folder[0] + "." + folder[1] + "." + folder[2]  + folder[3];
            else
                return folder[0] + "." + folder[1] + "." + folder[2]; ;
        }

        public string GetPath()
        {
            return folder[0] + "\\" + folder[1] + "\\" + folder[2] + "\\";
        }

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
                    break;
                case 10:
                    folder[0] = folderName.Substring(0, 2);
                    folder[1] = folderName.Substring(2, 4);
                    folder[2] = folderName.Substring(6, 3);
                    folder[3] = folderName.Substring(9, 1);
                    break;
                case 9:
                    folder[0] = folderName.Substring(0, 2);
                    folder[1] = folderName.Substring(2, 4);
                    folder[2] = folderName.Substring(6, 3);
                    break;
                default:
                    folder[0] = "00";
                    folder[1] = "0000";
                    folder[2] = "000";
                    break;
            }
            folder[0] = folder[0].ToUpper();
            folder[3] = folder[3].ToUpper();
            return folder;
        }

        private void SearchDataBase()
        {
            if ((folder[0] != "" && folder[1] != "" && folder[2] != "") && (folder[0] != null && folder[1] != null && folder[2] != null))
            {
                database = "rtec";
                name = "";
                GetName();
                if (name == "")
                {
                    database = "nord";
                    GetName();
                    if (name == "")
                    {
                        database = "";
                        name = "Impossible de trouver l'affaire !";
                        MessageBox.Show("Ce numéro d'affaire n'existe pas. Veuillez contrôler votre entrée ou créer l'affaire dans OSIRIS\r\n" +
                            "\r\n" +
                            "Affaire N°: " + GetFolderNr(), "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                name = "Impossible de trouver l'affaire !";
            }
        }

        private void GetName()
        {
            string connectionString = "";
            if (database == "nord")
                connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + nordPath + "; Persist Security Info = False";
            else if (database == "rtec")
                connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + rtecPath + "; Persist Security Info = False";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                OleDbCommand cmd = con.CreateCommand();
                cmd.Connection = con;
                try
                {
                    con.Open();
                    string query = "select * from Cht where Cht_Numero='" + GetFolderNr() + "'";
                    cmd.CommandText = query;

                    OleDbDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        name = rdr["Cht_Nom"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex, "ERROR : Database " + database, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public bool UpdateDataBase()
        {
            string connectionString = "";
            if (database == "nord")
                connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + nordPath + "; Persist Security Info = False";
            else if (database == "rtec")
                connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + rtecPath + "; Persist Security Info = False";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                OleDbCommand cmd = con.CreateCommand();
                cmd.Connection = con;
                try
                {
                    con.Open();
                    string query = "update Cht set Cht_Dossier='num.' ,CHT_BOITEINSTRUCTIONS='num.' where Cht_Numero='" + GetFolderNr() + "'";
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return true;
                }
            }
        }
    }
}
