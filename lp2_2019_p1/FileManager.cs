using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;

namespace lp2_2019_p1
{
    public class FileManager
    {
        private const string appName = "MyIMDBSearcher";
        private const string fileTitleBasics = "title.basics.tsv.gz";
        private string folderWithFiles = Path.Combine(
            Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData), appName);

        public FileManager()
        {
            OpenTitlesFile();
        }

        private void OpenTitlesFile()
        {
            string fileTitleBasicsFull = Path.Combine(
                folderWithFiles, fileTitleBasics);

            using (FileStream fs = new FileStream(
                fileTitleBasicsFull, FileMode.Open, FileAccess.Read))
            {
                using (GZipStream gzs = new GZipStream(
                    fs, CompressionMode.Decompress))
                {
                    using(StreamReader sr = new StreamReader(gzs))
                    {
                        string line;

                        sr.ReadLine();

                        while((line = sr.ReadLine()) != null)
                        {
                            
                        }
                    }
                }
            }
        }

        private void LineToTitle()
        {

        }
    }
}
