using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveScanTool
{
    class process
    {
        string path = Directory.GetCurrentDirectory();
        private string fileName;
        private string folder;

        public void ProcessFile()
        {
            string[] files = Directory.GetFiles(path, "*.pdf");
            foreach(string file in files)
            {
                //Extract name
                string[] folder = ExtractName(file);
                //Check folder

                //Check file
            }
        }
        
        private string[] ExtractName(string file)
        {
            string[] folder = new string[3];

            folder[0] = file.Substring(path.Length + 1, 2);
            folder[1] = file.Substring(path.Length + 4, 4);
            folder[2] = file.Substring(path.Length + 9, 3);

            return folder;
        }
    }
}
