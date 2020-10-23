using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ArchiveScanTool
{
    public partial class ChooseDataBase : Form
    {
        private string nordPath;
        private string rtecPath;

        public string NordPath
        {
            get { return nordPath; }
            set { nordPath = value; }
        }
        public string RtecPath
        {
            get { return rtecPath; }
            set { rtecPath = value; }
        }

        public ChooseDataBase()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            nordPath = textBoxPathNord.Text;
            rtecPath = textBoxPathRtec.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonBrowseNord_Click(object sender, EventArgs e)
        {
            if (openFileDialogPath.ShowDialog() == DialogResult.OK)
            {
                nordPath = Path.GetDirectoryName(openFileDialogPath.FileName);
                textBoxPathNord.Text = nordPath;
            }
        }

        private void buttonBrowseTec_Click(object sender, EventArgs e)
        {
            if (openFileDialogPath.ShowDialog() == DialogResult.OK)
            {
                rtecPath = Path.GetDirectoryName(openFileDialogPath.FileName);
                textBoxPathRtec.Text = rtecPath;
            }
        }

        private void ChooseDataBase_Load(object sender, EventArgs e)
        {
            textBoxPathNord.Text = nordPath;
            textBoxPathRtec.Text = rtecPath;
        }
    }
}
