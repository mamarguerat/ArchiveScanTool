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
using System.Xml;

namespace ArchiveScanTool
{
    public partial class Form1 : Form
    {
        List<Folders> folders = new List<Folders>();
        string workingPath = @"\\TERMINAL\Data\_ScannerTechnique";
        string foldersPath = @"\\TERMINAL\Data\chantiers";
        string configFile = @"C:\ProgramData\mamarguerat\ArchiveScanTool\config.txt";
        string configPath = @"C:\ProgramData\mamarguerat\ArchiveScanTool";
        string databaseNordPath = @"\\TERMINAL\Data\Osiris\nord\norddt.accdb";
        string databaseRtecPath = @"\\TERMINAL\Data\Osiris\rtec\rtecdt.accdb";
        bool cancelScript = false;
        string[,] comboboxList;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(configFile))
            {
                using (var sr = new StreamReader(configFile))
                {
                    workingPath = sr.ReadLine();
                    workingPath = workingPath.Substring(7);
                    foldersPath = sr.ReadLine();
                    foldersPath = foldersPath.Substring(12);
                    databaseNordPath = sr.ReadLine();
                    databaseNordPath = databaseNordPath.Substring(7);
                    databaseRtecPath = sr.ReadLine();
                    databaseRtecPath = databaseRtecPath.Substring(7);
                    List<string> filetype = new List<string>();
                    List<string> filename = new List<string>();
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        filetype.Add(line.Substring(11));
                        line = sr.ReadLine();
                        filename.Add(line.Substring(11));
                        line = sr.ReadLine();
                    }
                    comboboxList = new string[filetype.Count(), 2];
                    for (int i = 0; i < filetype.Count(); i++)
                    {
                        comboboxList[i, 0] = filetype[i];
                        comboboxList[i, 1] = filename[i];
                    }
                }
            }
            else
            {
                try
                {
                    DirectoryInfo di = Directory.CreateDirectory(configPath);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString(), "Erreur - vous n'avez pas les droits d'accès", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                SaveFile();
            }
            textBoxPath.Text = workingPath;
            for (int i = 0; i < comboboxList.Length / 2; i++)
            {
                comboBoxFileType.Items.Add(comboboxList[i, 0]);
            }
            this.ActiveControl = textBoxFolder;
            textBoxFolder.Focus();
        }

        private void SaveFile()
        {
            using (StreamWriter sw = new StreamWriter(configFile, false))
            {
                sw.WriteLine(@"Source:" + workingPath);
                sw.WriteLine(@"Destination:" + foldersPath);
                sw.WriteLine(@"norddt:" + databaseNordPath);
                sw.WriteLine(@"rtecdt:" + databaseRtecPath);
                string[] list = new string[] {
                    "00filetype:Commandes-contrats clients",
                    "00filename:contrat-client.pdf",
                    "01filetype:Doc fournisseurs",
                    "01filename:doc-fournisseur.pdf",
                    "02filetype:Factures clients",
                    "02filename:facture-client.pdf",
                    "03filetype:Factures fournisseurs",
                    "03filename:facture-fournisseur.pdf",
                    "04filetype:Plans",
                    "04filename:plan.pdf",
                    "05filetype:PV chantier",
                    "05filename:pv-chantier.pdf",
                    "06filetype:Rapport de travail",
                    "06filename:rapport-travail.pdf",
                    "07filetype:Schéma de principe",
                    "07filename:schema-principe.pdf"};
                comboboxList = new string[8, 2];
                comboboxList[0, 0] = "Commandes-contrats clients";
                comboboxList[0, 1] = "contrat-client.pdf";
                comboboxList[1, 0] = "Doc fournisseurs";
                comboboxList[1, 1] = "doc-fournisseur.pdf";
                comboboxList[2, 0] = "Factures clients";
                comboboxList[2, 1] = "facture-client.pdf";
                comboboxList[3, 0] = "Factures fournisseurs";
                comboboxList[3, 1] = "facture-fournisseur.pdf";
                comboboxList[4, 0] = "Plans";
                comboboxList[4, 1] = "plan.pdf";
                comboboxList[5, 0] = "PV chantier";
                comboboxList[5, 1] = "pv-chantier.pdf";
                comboboxList[6, 0] = "Rapport de travail";
                comboboxList[6, 1] = "rapport-travail.pdf";
                comboboxList[7, 0] = "Schéma de principe";
                comboboxList[7, 1] = "schema-principe.pdf";
                foreach (string element in list)
                {
                    sw.WriteLine(element);
                }
            }
        }

        private void aProposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("v1.1.1 - 20210121\r\n" +
                "\r\n" +
                "Programme développé par Martin Marguerat pour la société Nordvent SA\r\n" +
                "\r\n" +
                "Ce programme permet la gestion de la digitalisation des archives. Il automatise le processur de tri informatique, du changement de nom du fichier ainsi que la mise à jour des informations dans la base de données \"Osiris\"\r\n" +
                "\r\n" +
                "© mamarguerat - 2020-2021 - martinmarguerat.ch\r\n" +
                "\r\n" +
                "Support:\r\n" +
                "✉ martin@marguerat.ch\r\n" +
                "📞 +41 79 748 86 27",
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
                    ResetFields();
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
                    i--;
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
                    folders.Add(new Folders(strs[i], databaseNordPath, databaseRtecPath));
                }
                brk = false;
            }
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialogPath.ShowDialog() == DialogResult.OK)
            {
                workingPath = Path.GetDirectoryName(openFileDialogPath.FileName);
                SaveFile();
                textBoxPath.Text = workingPath;
                folders.Clear();
                listBoxFiles.Items.Clear();
                ResetFields();
            }
        }

        private void listBoxFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFields();
            textBoxFolder.Select();
            textBoxFolder.Focus();
        }

        private void UpdateFields()
        {
            Folders selectedFolder = folders[listBoxFiles.SelectedIndex];
            textBoxName.Text = selectedFolder.Name;
            comboBoxFileType.SelectedIndex = comboBoxFileType.FindStringExact(selectedFolder.FileType);
            textBoxFileName.Text = selectedFolder.File;
            if (selectedFolder.Name != "Impossible de trouver l'affaire !")
            {
                textBoxFolder.Text = selectedFolder.GetFolderNr();
                string newName = GetNewName(selectedFolder);
                textBoxDestination.Text = foldersPath + "\\" + selectedFolder.GetPath() + "Archives scannées\\" + newName;
                textBoxNewFileName.Text = newName;
            }
            else
            {
                textBoxFolder.Text = "";
                textBoxDestination.Text = "";
                textBoxNewFileName.Text = "";
            }
            axAcroPDF.Visible = true;
            axAcroPDF.src = workingPath + "\\" + selectedFolder.File;
            textBoxFolder.Select();
            textBoxFolder.Focus();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            UpdateFolders();
            textBoxFolder.Select();
            textBoxFolder.Focus();
        }

        private void textBoxFolder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                UpdateFolders();
                textBoxFolder.Select();
                textBoxFolder.Focus();
            }
        }

        private void UpdateFolders()
        {
            folders[listBoxFiles.SelectedIndex].FolderName(textBoxFolder.Text);
            folders[listBoxFiles.SelectedIndex].FileType = comboBoxFileType.Text;
            UpdateList();
            UpdateFields();
            textBoxFolder.Select();
            textBoxFolder.Focus();
        }

        private string GetNewName(Folders folder)
        {
            string extension = "";
            for (int i = 0; i < comboboxList.Length / 2; i++)
            {
                if (folder.FileType == comboboxList[i, 0])
                {
                    extension += "-" + comboboxList[i, 1];
                }
            }
            return folder.GetFolderNr() + extension;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            UpdateFields();
            textBoxFolder.Select();
            textBoxFolder.Focus();
        }

        private void ResetFields()
        {
            textBoxName.Text = "";
            textBoxFolder.Text = "";
            textBoxDestination.Text = "";
            textBoxFileName.Text = "";
            textBoxNewFileName.Text = "";
            comboBoxFileType.Text = "";
            axAcroPDF.Visible = false;
        }

        private void changerLeDossierDeDestinationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogPath.ShowDialog() == DialogResult.OK)
            {
                foldersPath = Path.GetDirectoryName(openFileDialogPath.FileName);
                SaveFile();
            }
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonRunScript_Click(object sender, EventArgs e)
        {
            //Afficher ProgressBar
            listBoxFiles.Enabled = false;
            textBoxFolder.Enabled = false;
            textBoxPath.Enabled = false;
            comboBoxFileType.Enabled = false;
            buttonCancel.Enabled = false;
            buttonUpdate.Enabled = false;
            buttonImport.Enabled = false;
            buttonBrowse.Enabled = false;
            buttonCancelScript.Visible = true;
            buttonRunScript.Visible = false;
            axAcroPDF.Visible = false;
            progressBarScript.Maximum = folders.Count();
            string errorPath = workingPath + "\\error";
            //pour chaque élément de la liste
            foreach (Folders fld in folders)
            {
                //Trouver le chemin de destination
                string destination = foldersPath + "\\" + fld.GetPath() + "Archives scannées\\";
                string fileDestination = destination + GetNewName(fld);
                string fileSource = workingPath + "\\" + fld.File;
                //Créer les dossiers s'ils n'existent pas
                try
                {
                    DirectoryInfo di = Directory.CreateDirectory(destination);
                    //Contrôler que le fichier n'y est pas déjà
                    bool error = fld.UpdateDataBase();
                    if (File.Exists(fileDestination) || error)
                    {
                        //error
                        DirectoryInfo de = Directory.CreateDirectory(errorPath);
                        File.Move(fileSource, errorPath + "\\" + fld.File);
                    }
                    else
                    {
                        //Déplacer le fichier
                        File.Move(fileSource, fileDestination);
                    }
                    progressBarScript.Increment(1);
                    if (cancelScript)
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Erreur - vous n'avez pas les droits d'accès", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DirectoryInfo de = Directory.CreateDirectory(errorPath);
                    File.Move(fileSource, errorPath + "\\" + fld.File);
                }
            }
            string[] errorFiles;
            try
            {
                errorFiles = Directory.GetFiles(errorPath, "*.pdf");
            }
            catch (Exception ex)
            {
                errorFiles = null;
            }
            if (errorFiles == null || errorFiles.Length == 0)
                MessageBox.Show("Les fichiers on tous étés triés avec succès !", "Terminé", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Certains fichiers rencontrent une erreur. Les fichiers concernés se trouvent dans le dossier \"erreur\" à l'emplacement d'origine", "Terminé avec erreurs", MessageBoxButtons.OK, MessageBoxIcon.Error);
            folders.Clear();
            listBoxFiles.Enabled = true;
            textBoxFolder.Enabled = true;
            textBoxPath.Enabled = true;
            comboBoxFileType.Enabled = true;
            buttonCancel.Enabled = true;
            buttonUpdate.Enabled = true;
            buttonImport.Enabled = true;
            buttonBrowse.Enabled = true;
            buttonRunScript.Visible = true;
            buttonCancelScript.Visible = false;
            axAcroPDF.Visible = true;
            progressBarScript.Value = 0;
            cancelScript = false;
            UpdateList();
            }

            private void buttonCancelScript_Click(object sender, EventArgs e)
            {
                cancelScript = true;
            }

            private void changerLemplacementDesBasesDeDonnéesToolStripMenuItem_Click(object sender, EventArgs e)
            {
                ChooseDataBase chooseDB = new ChooseDataBase();
                chooseDB.NordPath = databaseNordPath;
                chooseDB.RtecPath = databaseRtecPath;
                if (chooseDB.ShowDialog() == DialogResult.OK)
                {
                    databaseNordPath = chooseDB.NordPath;
                    databaseRtecPath = chooseDB.RtecPath;
                    foreach (Folders fld in folders)
                    {
                        fld.NordPath = databaseNordPath;
                        fld.RtecPath = databaseRtecPath;
                    }
                    SaveFile();
                }
            }

        private void axAcroPDF_Validated(object sender, EventArgs e)
        {
            textBoxFolder.Select();
            textBoxFolder.Focus();
        }
    }

    }
