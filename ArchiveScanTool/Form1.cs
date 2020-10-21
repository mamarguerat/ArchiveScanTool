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
        string workingPath;
        process process = new process();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            workingPath = Directory.GetCurrentDirectory();
            textBoxPath.Text = workingPath;
            //process.ProcessFile();
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialogPath.ShowDialog() == DialogResult.OK)
            {
                workingPath = Path.GetDirectoryName(openFileDialogPath.FileName);
                textBoxPath.Text = workingPath;
                //Clear file list !!!!!
            }
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            FileSelection fileSelection = new FileSelection();
            string[] files = Directory.GetFiles(workingPath, "*.pdf");
            string[] fileNames = new string[files.Length];
            for (int i= 0; i<files.Length;i++)
            {
                fileNames[i] = files[i].Substring(workingPath.Length + 1);
            }
            fileSelection.FileNames = fileNames;
            fileSelection.ShowDialog();
        }
    }
}
