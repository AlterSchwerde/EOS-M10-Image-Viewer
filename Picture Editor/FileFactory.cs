using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Picture_Editor
{
    class FileFactory
    {

        public static List<string> GetFilenames(DragEventArgs e)
        {
            List<string> pictureFilenames = new List<string>();

            string[] filenames = (string[]) e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string filename in filenames)
            {
                string extension = Path.GetExtension(filename);
                if (extension == ".jpg" || extension == ".JPG")
                    pictureFilenames.Add(filename);
            }

            return pictureFilenames;
        }



        #region 
        public static List<string> RenameFiles(List<string> filenames, string newFilename)
        {
            List<string> newFilenames = new List<string>();

            if (filenames != null && filenames.Count != 0)
            {
                int i = 1;
                foreach (string filename in filenames)
                {
                    string directory = Path.GetDirectoryName(filename);
                    string index = i.ToString();

                    while (index.Length < 4)
                    {
                        index = "0" + index;
                    }
                    index = "_" + index;

                    string newName = directory + @"\" + newFilename + index + ".jpg";
                    try
                    {
                        File.Move(filename, newName); // Try to move
                        //Console.WriteLine("Moved"); // Success
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.ToString()); // Write error
                    }

                    newFilenames.Add(newName);

                    i = i + 1;
                }
            }

            return newFilenames;
        }
        #endregion


    }
}


