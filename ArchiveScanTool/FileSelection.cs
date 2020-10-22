using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArchiveScanTool
{
    public partial class FileSelection : Form
    {
        private string[] fileNames;
        private string[] selectedFiles;

        public string[] FileNames
        {
            set { fileNames = value; }
        }

        public string[] SelectedFiles
        {
            get { return selectedFiles; }
            set { selectedFiles = value; }
        }

        public FileSelection()
        {
            InitializeComponent();
        }

        private void FileSelection_Load(object sender, EventArgs e)
        {
            string[] sltFls = new string[selectedFiles.Length];
            selectedFiles.CopyTo(sltFls, 0);
            for (int i = 0; i < sltFls.Length; i++)
            {
                if (sltFls[i].Contains(" | "))
                {
                    sltFls[i] = sltFls[i].Split('|')[0];
                    sltFls[i] = sltFls[i].Remove(sltFls[i].Length - 1);
                }
            }
            foreach (string file in fileNames)
            {
                if (!sltFls.Contains(file))
                    listBoxFiles.Items.Add(file);
            }
            foreach (string file in selectedFiles)
            {
                listBoxSelected.Items.Add(file);
            }
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            foreach (int selected in listBoxFiles.SelectedIndices)
            {
                listBoxSelected.Items.Add(listBoxFiles.Items[selected]);
            }
            if (listBoxFiles.SelectedIndex != -1)
            {
                for (int i = listBoxFiles.SelectedItems.Count - 1; i >= 0; i--)
                    listBoxFiles.Items.Remove(listBoxFiles.SelectedItems[i]);
            }

        }

        private void buttonUnselect_Click(object sender, EventArgs e)
        {
            foreach (int selected in listBoxSelected.SelectedIndices)
            {
                listBoxFiles.Items.Add(listBoxSelected.Items[selected]);
            }

            if (listBoxSelected.SelectedIndex != -1)
            {
                for (int i = listBoxSelected.SelectedItems.Count - 1; i >= 0; i--)
                    listBoxSelected.Items.Remove(listBoxSelected.SelectedItems[i]);
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            selectedFiles = new string[listBoxSelected.Items.Count];
            for (int i = 0; i < listBoxSelected.Items.Count; i++)
            {
                selectedFiles[i] = listBoxSelected.Items[i].ToString();
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
