using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Globalization;

namespace lp2_2019_p1
{
    /// <summary>
    /// Class that gets our files, reads our files, and puts them in their
    /// respective Dictionaries. Basically, our database!
    /// 
    /// Most of the code here is taken from the 'sample' included in the
    /// project as reference.
    /// </summary>
    public class FileManager
    {
        // Our constants to check for files.
        private const string appName = "MyIMDBSearcher";
        private const string fileTitleBasics = "title.basics.tsv.gz";
        private const string fileRatingsBasics = "title.ratings.tsv.gz";
        private string folderWithFiles, fileTitleBasicsFull, 
            fileRatingsBasicsFull;

        // Our Database for Titles, complete with all of our information.
        public Dictionary<string, StructTitle> Titles { get; private set; }

        // Our Database for Ratings, complete with all of our information.
        public Dictionary<string, StructRatings> Ratings { get; private set; }

        // Our set to contain all possible genres.
        public ISet<string> AllGenres { get; private set; }

        // Our set to contain all possible types.
        public ISet<string> AllTypes { get; private set; }

        // Integers used for measuring the maximum size of our Dictionaries.
        private int numTitles = 0;
        private int numRatings = 0;

        /// <summary>
        /// Constructor to initialize our database. Will read through
        /// each file and put it into a list.
        /// </summary>
        public FileManager()
        {
            // Set the paths.
            // Folder to read from.
            folderWithFiles = Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData), appName);

            // The full file name of our Titles file.
            fileTitleBasicsFull = Path.Combine(
                folderWithFiles, fileTitleBasics);

            // The full file name of our Ratings file.
            fileRatingsBasicsFull = Path.Combine(
                folderWithFiles, fileRatingsBasics);

            // Count the lines in the Titles file.
            OpenTitlesFile((line) => numTitles++);

            // Create our Titles Dictionary with a maximum.
            Titles = new Dictionary<string, StructTitle>(numTitles);

            // Create our set for genres.
            AllGenres = new HashSet<string>();

            // Create our set for types.
            AllTypes = new HashSet<string>();

            // Read the information from Titles and add it to our Dictionary.
            OpenTitlesFile(AddInformationToTitles);

            // Count the lines in the Ratings file.
            OpenRatingsFile((line) => numRatings++);

            // Create our Ratings Dictionary with a maximum.
            Ratings = new Dictionary<string, StructRatings>(numRatings);

            // Read the information from Ratings and add it to our Dictionary.
            OpenRatingsFile(AddInformationToRatings);
        }

        /// <summary>
        /// Method to open our Titles file and read from it.
        /// </summary>
        /// <param name="actionForEachLine">Method to call for each line.</param>
        private void OpenTitlesFile(Action<string> actionForEachLine)
        {
            // Get the FileStream, the GZIP and the StreamReader...
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

        /// <summary>
        /// Method to open our Ratings file and read from it.
        /// </summary>
        /// <param name="actionForEachLine">Method to call for each line.</param>
        private void OpenRatingsFile(Action<string> actionForEachLine)
        {
            // Get the FileStream, the GZIP and the StreamReader...
            using (FileStream fs = new FileStream(
                fileRatingsBasicsFull, FileMode.Open, FileAccess.Read))
            {
                using (GZipStream gzs = new GZipStream(
                    fs, CompressionMode.Decompress))
                {
                    using (StreamReader sr = new StreamReader(gzs))
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

        /// <summary>
        /// Add the information from the Titles file into our
        /// Titles Dictionary.
        /// </summary>
        /// <param name="line">Each line of the file.</param>
        private void AddInformationToTitles(string line)
        {
            /* 
             * Variables to use for assigning information.
             * By default, the Titles file goes as follows with the index...
             * 0 = Identifier.
             * 1 = Type.
             * 2 = Primary title name.
             * 3 = Original name.
             * 4 = For adults or not.
             * 5 = The release year.
             * 6 = The end year.
             * 7 = Runtime in minutes.
             * 8 = The three (possible) genres associated with the title.
             */
            short aux;
            string[] fields = line.Split("\t");
            string titleIdentifier = fields[0];
            string titleType = fields[1];
            string titlePrimaryTitle = fields[2];
            bool titleAdult = fields[4] == "0" ? false : true;
            short? titleStartYear, titleEndYear;
            string[] titleGenres = fields[8].Split(",");

            // Our clean Titles and Types.
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

            // Create our Titles entry.
            StructTitle t = new StructTitle(
                titleIdentifier, titleType, titlePrimaryTitle, titleAdult,
                titleStartYear, titleEndYear, cleanTitleGenres.ToArray());
            
            // Add it to our Dictionary. If the one being inserted matches our
            // identifier, we will replace it with the new one in order to
            // avoid duplicates, saving up memory!
            Titles[t.TitleIdentifier] = t;
        }

        /// <summary>
        /// Add the information from the Ratings file into our
        /// Ratings Dictionary.
        /// </summary>
        /// <param name="line">Each line of the file.</param>
        private void AddInformationToRatings(string line)
        {
            /*
             * Variables to use for assigning information.
             * By default, the Ratings file goes as follows with the index...
             * 0 = Identifier.
             * 1 = Average rating.
             * 2 = Number of votes.
             */
            string[] fields = line.Split("\t");
            string titleIdentifier = fields[0];
            float averageRating = float.Parse(
                fields[1].Replace('.', ','),
                CultureInfo.GetCultureInfo("pt-PT"));

            // Create our Ratings entry.
            StructRatings r = new StructRatings(
                titleIdentifier, averageRating);

            // Add it to our Dictionary, checking for equal identifiers, and
            // adding our entry on top of it so we avoid duplicates.
            Ratings[r.RatingsIdentifier] = r;
        }
    }
}
