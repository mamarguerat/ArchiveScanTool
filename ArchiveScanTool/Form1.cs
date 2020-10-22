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
    public partial class Form1 : Form
    {
        List<Folders> folders = new List<Folders>();
        string workingPath;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            workingPath = Directory.GetCurrentDirectory();
            textBoxPath.Text = workingPath;
            comboBoxFileType.Items.Add("Général");
            comboBoxFileType.Items.Add("Doc fournisseurs");
            comboBoxFileType.Items.Add("e-mail");
            comboBoxFileType.Items.Add("Photos");
            comboBoxFileType.Items.Add("Plans C-S-E");
            comboBoxFileType.Items.Add("Plans d'architecte");
            comboBoxFileType.Items.Add("Plans de ventilation");
            comboBoxFileType.Items.Add("PV chantier");
            comboBoxFileType.Items.Add("Schéma de principe");
        }

        private void aProposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("v0.0.1 - 20201012\r\n" +
                "\r\n" +
                "Programme développé par Martin Marguerat pour la société Nordvent SA\r\n" +
                "\r\n" +
                "Ce programme permet la gestion de la digitalisation des archives. Il automatise le processur de tri informatique, du changement de nom du fichier ainsi que la mise à jour des informations dans la base de données \"Osiris\"\r\n" +
                "\r\n" +
                "© mamarguerat - 2020",
                "A propos", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            //Open file selection Form
            FileSelection fileSelection = new FileSelection();
            //Listing files available
            string[] files = Directory.GetFiles(workingPath, "*.pdf");
            //Getting the file name ONLY
            string[] fileNames = new string[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                fileNames[i] = files[i].Substring(workingPath.Length + 1);
            }
            fileSelection.FileNames = fileNames;
            fileSelection.SelectedFiles = new string[listBoxFiles.Items.Count];
            for (int i = 0; i < listBoxFiles.Items.Count; i++)
            {
                fileSelection.SelectedFiles[i] = listBoxFiles.Items[i].ToString();
            }
            if (fileSelection.ShowDialog() == DialogResult.OK)
            {
                CompareList(fileSelection.SelectedFiles);
                UpdateList();
            }
        }

        private void UpdateList()
        {
            int selected = listBoxFiles.SelectedIndex;
            listBoxFiles.Items.Clear();
            foreach (Folders fld in folders)
            {
                if (fld.Name != "Impossible de trouver l'affaire !")
                    listBoxFiles.Items.Add(fld.File + " | " + GetNewName(fld));
                else
                    listBoxFiles.Items.Add(fld.File);
            }
            try
            {
                listBoxFiles.SelectedIndex = selected != -1 ? selected : 0;

            }
            catch
            {
                try
                {
                    listBoxFiles.SelectedIndex = 0;
                }
                catch
                {

                }
            }
        }

        private void CompareList(string[] strs)
        {
            for (int i = 0; i < strs.Length; i++)
            {
                if (strs[i].Contains(" | "))
                {
                    strs[i] = strs[i].Split('|')[0];
                    strs[i] = strs[i].Remove(strs[i].Length - 1);
                }
            }
            bool brk = false;
            for (int i = 0; i < folders.Count; i++)
            {
                for (int j = 0; j < strs.Length; j++)
                {
                    if (folders[i].File == strs[j])
                    {
                        brk = true;
                        break;
                    }
                }
                if (!brk)
                {
                    folders.RemoveAt(i);
                }
                brk = false;
            }
            for (int i = 0; i < strs.Length; i++)
            {
                for (int j = 0; j < folders.Count; j++)
                {
                    if (folders[j].File == strs[i])
                    {
                        brk = true;
                        break;
                    }
                }
                if (!brk)
                {
                    folders.Add(new Folders(strs[i]));
                }
                brk = false;
            }
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialogPath.ShowDialog() == DialogResult.OK)
            {
                workingPath = Path.GetDirectoryName(openFileDialogPath.FileName);
                textBoxPath.Text = workingPath;
                //Clear file list !!!!!
            }
        }

        private void listBoxFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFields();
        }

        private void UpdateFields()
        {
            Folders selectedFolder = folders[listBoxFiles.SelectedIndex];
            textBoxName.Text = selectedFolder.Name;
            comboBoxFileType.SelectedIndex = comboBoxFileType.FindStringExact(selectedFolder.FileType);
            textBoxFileName.Text = selectedFolder.File;
            if (selectedFolder.Name != "Impossible de trouver l'affaire !")
            {
                textBoxFolder.Text = selectedFolder.GetFolderName();
                string newName = GetNewName(selectedFolder);
                textBoxDestination.Text = @"\\TERMINAL\Data\Chantiers\" + selectedFolder.GetPath() + "Archives scannées\\" + newName;
                textBoxNewFileName.Text = newName;
            }
            else
            {
                textBoxFolder.Text = "";
                textBoxDestination.Text = "";
                textBoxNewFileName.Text = "";
            }
            axAcroPDF.src = workingPath + "\\" + selectedFolder.File;
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            UpdateFolders();
        }

        private void textBoxFolder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                UpdateFolders();
            }
        }

        private void UpdateFolders()
        {
            folders[listBoxFiles.SelectedIndex].FolderName(textBoxFolder.Text);
            folders[listBoxFiles.SelectedIndex].FileType = comboBoxFileType.Text;
            UpdateList();
            UpdateFields();
        }

        private string GetNewName(Folders folder)
        {
            string extension = "";
            //Switch case from combobox
            switch (folder.FileType)
            {
                case "Général":
                    extension = "";
                    break;
                case "Doc fournisseurs":
                    extension = "-doc-fournisseurs";
                    break;
                case "e - mail":
                    extension = "-e-mail";
                    break;
                case "Photos":
                    extension = "-photos";
                    break;
                case "Plans C-S-E":
                    extension = "-plans-C-S-E";
                    break;
                case "Plans d'architecte":
                    extension = "-plans-architecte";
                    break;
                case "Plans de ventilation":
                    extension = "-plans-ventilation";
                    break;
                case "PV chantier":
                    extension = "-PV-chantier";
                    break;
                case "Schéma de principe":
                    extension = "-schema-principe";
                    break;
            }
            return folder.GetFolderName() + extension + ".pdf";
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            UpdateFields();
        }
    }
}
