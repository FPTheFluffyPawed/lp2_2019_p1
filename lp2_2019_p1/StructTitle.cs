using System;
using System.Collections.Generic;
using System.Text;

namespace lp2_2019_p1
{
    public struct StructTitle
    {
        // Unique identifier.
        public string TitleIdentifier// { get; }
            => Line.Split("\t")[0];

        // Type of title.
        public string TitleType //{ get; }
            => Line.Split("\t")[1];

        // Primary name of the title.
        public string PrimaryTitle //{ get; }
            => Line.Split("\t")[2];

        // If its for adults or not.
        public bool ForAdults //{ get; }
            => bool.Parse(Line.Split("\t")[3]);

        // The start year (if it exists).
        public short? StartYear //{ get; }
        {
            get
            {

                if (short.TryParse(Line.Split("\t")[4], out short value))
                    return value;

                return null;
            }
        }

        // The end year (if it exists).
        public short? EndYear
        {
            get
            {

                if (short.TryParse(Line.Split("\t")[5], out short value))
                    return value;

                return null;
            }
        }

        // Title's genres.
        public IEnumerable<string> Genres //{ get; }
            => Line.Split("\t")[6].Split(",");

        // Line to assign values, and for dividing usage.
        private string Line { get; }

        public StructTitle(
            string titleIdentifier, string titleType, string primaryTitle,
            bool forAdults, short? startYear, short? endYear,
            IEnumerable<string> genres)
        {
            Line = string.Join('\t',
                new string[]
                {
                    titleIdentifier,
                    titleType,
                    primaryTitle,
                    forAdults.ToString(),
                    startYear?.ToString(),
                    endYear?.ToString(),
                    string.Join(',', genres)
                });
        }
    }
}
