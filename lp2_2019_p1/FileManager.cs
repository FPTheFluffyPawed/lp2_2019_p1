using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace lp2_2019_p1
{
    public class FileManager
    {
        private const string appName = "MyIMDBSearcher";
        private const string fileTitleBasics = "title.basics.tsv.gz";
        private const string fileRatingsBasics = "title.ratings.tsv.gz";
        private string folderWithFiles, fileTitleBasicsFull, 
            fileRatingsBasicsFull;

        // Our Database for Titles complete with all of our information.
        public ICollection<StructTitle> Titles { get; private set; }

        // Our Database for Ratings complete with all of our information.
        public ICollection<StructRatings> Ratings { get; private set; }

        // Our set to contain all possible genres.
        public ISet<string> AllGenres { get; private set; }

        // Our set to contain all possible types.
        public ISet<string> AllTypes { get; private set; }

        private int numTitles = 0;
        private int numRatings = 0;

        public FileManager()
        {
            // Set the paths.
            folderWithFiles = Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData), appName);

            fileTitleBasicsFull = Path.Combine(
                folderWithFiles, fileTitleBasics);

            fileRatingsBasicsFull = Path.Combine(
                folderWithFiles, fileRatingsBasics);

            // Count the lines in the Titles file.
            OpenTitlesFile((line) => numTitles++);

            // Create our list with a maximum.
            Titles = new List<StructTitle>(numTitles);

            // Create our set for genres.
            AllGenres = new HashSet<string>();

            // Create our set for types.
            AllTypes = new HashSet<string>();

            // Read the information from Titles and add it to our collection.
            OpenTitlesFile(AddInformationToTitles);

            // Count the lines in the Ratings file.
            OpenRatingsFile((line) => numRatings++);

            // Create our Ratings list with a maximum.
            Ratings = new List<StructRatings>(numRatings);

            // Read the information from Ratings and add it to our collection.
            OpenRatingsFile(AddInformationToRatings);
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

        private void OpenRatingsFile(Action<string> actionForEachLine)
        {
            using (FileStream fs = new FileStream(
                fileRatingsBasicsFull, FileMode.Open, FileAccess.Read))
            {
                using (GZipStream gzs = new GZipStream(
                    fs, CompressionMode.Decompress))
                {
                    using (StreamReader sr = new StreamReader(gzs))
                    {
                        string line;

                        sr.ReadLine();

                        while((line = sr.ReadLine()) != null)
                        {
                            actionForEachLine.Invoke(line);
                        }
                    }
                }
            }
        }

        private void AddInformationToTitles(string line)
        {
            /*
             * 0 = identifier, 1 = type, 2 = primaryTitle, 3 = originalTitle
             * 4 = isAdult, 5 = startYear, 6 = endYear, 7 = runtimeMinutes
             * 8 = genres
             */

            short aux;
            string[] fields = line.Split("\t");
            string titleIdentifier = fields[0];
            string titleType = fields[1];
            string titlePrimaryTitle = fields[2];
            bool titleAdult = fields[4] == "0" ? false : true;
            short? titleStartYear, titleEndYear;
            string[] titleGenres = fields[8].Split(",");
            ICollection<string> cleanTitleGenres = new List<string>();
            ICollection<string> cleanTitleTypes = new List<string>();

            // Try to get the start year.
            try
            {
                titleStartYear = short.TryParse(fields[5], out aux)
                    ? (short?)aux
                    : null;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(
                    $"Tried to parse '{line}', but got exception '{e.Message}'"
                    + $" with this stack trace: {e.StackTrace}");
            }

            // Try to get the end year.
            try
            {
                titleEndYear = short.TryParse(fields[6], out aux)
                    ? (short?)aux
                    : null;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(
                    $"Tried to parse '{line}', but got exception '{e.Message}'"
                    + $" with this stack trace: {e.StackTrace}");
            }

            // Remove the invalid types.
            if (titleType != null && titleType != @"\N")
                cleanTitleTypes.Add(titleType);

            // Add the valid types to the set of all types in the database.
            foreach (string type in cleanTitleTypes)
                AllTypes.Add(type);

            // Remove invalid genres.
            foreach (string genre in titleGenres)
                if (genre != null && genre.Length > 0 && genre != @"\N")
                    cleanTitleGenres.Add(genre);

            // Add the valid genres to the set of all genres in the database.
            foreach (string genre in cleanTitleGenres)
                AllGenres.Add(genre);

            StructTitle t = new StructTitle(
                titleIdentifier, titleType, titlePrimaryTitle, titleAdult,
                titleStartYear, titleEndYear, cleanTitleGenres.ToArray());

            Titles.Add(t);
        }

        private void AddInformationToRatings(string line)
        {
            string[] fields = line.Split("\t");
            string titleIdentifier = fields[0];
            float averageRating = float.Parse(fields[1]);

            StructRatings r = new StructRatings(
                titleIdentifier, averageRating);

            Ratings.Add(r);
        }
    }
}
