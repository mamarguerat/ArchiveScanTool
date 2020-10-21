using System;
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
                int fileNameLength = fileName.Length;
                string[] folder = ExtractName(fileName, fileNameLength);
                if (folder[0] != "00" && folder[1] != "0000" && folder[2] != "000")
                {
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

        private string[] ExtractName(string file, int fileNameLength)
        {
            string[] folder = new string[3];

            switch (fileNameLength)
            {
                case 15:
                    folder[0] = file.Substring(0, 2);
                    folder[1] = file.Substring(3, 4);
                    folder[2] = file.Substring(8, 3);
                    break;
                case 12:
                    folder[0] = file.Substring(0, 2);
                    folder[1] = file.Substring(2, 4);
                    folder[2] = file.Substring(6, 3);
                    break;
                default:
                    folder[0] = "00";
                    folder[1] = "0000";
                    folder[2] = "000";
                    break;
            }
            folder[0] = folder[0].ToUpper();
            return folder;
        }
    }
}
