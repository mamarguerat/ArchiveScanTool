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
            //comboBoxFileType.SelectedIndex = comboBoxFileType.FindStringExact("Général");
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
            FileSelection fileSelection = new FileSelection();
            string[] files = Directory.GetFiles(workingPath, "*.pdf");
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
            listBoxFiles.Items.Clear();
            foreach (Folders fld in folders)
            {
                if (fld.Name != "Impossible de trouver l'affaire !")
                    listBoxFiles.Items.Add(fld.File + " (" + fld.GetFolderName() + ")");
                else
                    listBoxFiles.Items.Add(fld.File);
            }
        }

        private void CompareList(string[] strs)
        {
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
            Folders selectedFolder = folders[listBoxFiles.SelectedIndex];
            textBoxName.Text = selectedFolder.Name;
            comboBoxFileType.SelectedIndex = comboBoxFileType.FindStringExact(selectedFolder.FileType);
            textBoxFileName.Text = selectedFolder.File;
            if (selectedFolder.Name != "Impossible de trouver l'affaire !")
            {
                textBoxFolder.Text = selectedFolder.GetFolderName();
                //textBoxDestination.Text = 
                //textBoxNewFileName.Text = 
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
            UpdateFields();
        }

        private void textBoxFolder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                UpdateFields();
            }
        }

        private void UpdateFields()
        {
            folders[listBoxFiles.SelectedIndex].FolderName(textBoxFolder.Text);
            UpdateList();
        }
    }
}
