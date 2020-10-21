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

namespace ArchiveScanTool
{
    public partial class Form1 : Form
    {
        process process = new process();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            process.ProcessFile();
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
    }
}
