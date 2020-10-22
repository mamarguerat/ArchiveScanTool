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

        public string File
        {
            get { return file; }
            set { file = value; }
        }
        public string[] Folder
        {
            get { return folder; }
            set { folder = value; }
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

        public string SearchDataBase(string[] folder)
        {
            //string connectionString = "Driver ={ Microsoft Access Driver(*.mdb, *.accdb)}; Dsn=" + database + ";";
            database = "rtec";
            string name = "";
            name = GetName(folder);
            if (name != "")
            {
                MessageBox.Show("Name: " + name, "Found in " + database);
                return name;
            }
            database = "nord";
            name = GetName(folder);
            if (name != "")
            {
                MessageBox.Show("Name: " + name, "Found in " + database);
                return name;
            }
            database = "";
            return name;
        }

        public string GetName(string[] folder)
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
