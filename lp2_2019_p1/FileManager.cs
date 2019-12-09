using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace lp2_2019_p1
{
    public class FileManager
    {
        private const string appName = "MyIMDBSearcher";
        private const string fileTitleBasics = "title.basics.tsv.gz";
        private string folderWithFiles, fileTitleBasicsFull;
        public ICollection<StructTitle> Titles { get; set; }

        private int numTitles = 0;

        public FileManager()
        {
            // Set the paths.
            folderWithFiles = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), appName);
            fileTitleBasicsFull = Path.Combine(folderWithFiles, fileTitleBasics);

            // Count the lines in the file.
            OpenTitlesFile((line) => numTitles++);

            // Create our list with a maximum.
            Titles = new List<StructTitle>(numTitles);

            // Read the information from file and add it to our collection.
            OpenTitlesFile(AddInformationToList);
        }

        private void OpenTitlesFile(Action<string> actionForEachLine)
        {


            using (FileStream fs = new FileStream(
                fileTitleBasicsFull, FileMode.Open, FileAccess.Read))
            {
                using (GZipStream gzs = new GZipStream(
                    fs, CompressionMode.Decompress))
                {
                    using(StreamReader sr = new StreamReader(gzs))
                    {
                        string line;

                        // Skip the first line of the folder.
                        sr.ReadLine();

                        // Read through every line.
                        while((line = sr.ReadLine()) != null)
                        {
                            actionForEachLine.Invoke(line);
                        }
                    }
                }
            }
        }

        private void AddInformationToList(string line)
        {
            /*
             * 0 = identifier, 1 = type, 2 = primaryTitle, 3 = originalTitle
             * 4 = isAdult, 5 = startYear, 6 = endYear, 7 = runtimeMinutes
             * 8 = genres
             */

            short aux;
            string[] fields = line.Split("\t");
            string[] titleGenres;
        }
    }
}
