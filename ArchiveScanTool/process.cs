﻿using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using System.Data.OleDb;

namespace ArchiveScanTool
{
    class process
    {

        public void ProcessFile()
        {
            string path = Directory.GetCurrentDirectory();
            string[] files = Directory.GetFiles(path, "*.pdf");
            foreach (string file in files)
            {
                //Extract name
                string fileName = file.Substring(path.Length + 1);
                if (fileName.Length == 15)
                {
                    string[] folder = ExtractName(fileName);
                    string newFileName = folder[0] + "." + folder[1] + "." + folder[2] + "-scan.pdf";
                    //string newPath = "\\TERMINAL\\Data\\Chantiers\\" + folder[0] + "\\" + folder[1] + "\\" + folder[2] + "\\" + newFileName;
                    string newPath = path + "\\ok\\" + newFileName;
                    int temp;
                    //Check folder

                    //Check file
                    if (int.TryParse(folder[1], out temp) && int.TryParse(folder[2], out temp))
                    {
                        if (File.Exists(newPath))
                        {
                            //error
                        }
                        else
                        {
                            //Move and rename
                            File.Move(file, newPath);
                        }
                    }
                }
            }
        }

        private string[] ExtractName(string file)
        {
            string[] folder = new string[3];

            folder[0] = file.Substring(0, 2);
            folder[1] = file.Substring(3, 4);
            folder[2] = file.Substring(8, 3);

            return folder;
        }
    }
}
