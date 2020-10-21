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

        public string[] FileNames
        {
            set { fileNames = value; }
        }
        public FileSelection()
        {
            InitializeComponent();
        }

        private void FileSelection_Load(object sender, EventArgs e)
        {
            foreach(string file in fileNames)
            {
                listBoxFiles.Items.Add(file);
            }
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            foreach(int selected in listBoxFiles.SelectedIndices)
            {
                listBoxSelected.Items.Add(listBoxFiles.Items[selected]);
                listBoxFiles.Items.RemoveAt(selected);
            }
        }

        private void buttonUnselect_Click(object sender, EventArgs e)
        {
            foreach (int selected in listBoxSelected.SelectedIndices)
            {
                listBoxFiles.Items.Add(listBoxSelected.Items[selected]);
                listBoxSelected.Items.RemoveAt(selected);
            }
        }
    }
}
