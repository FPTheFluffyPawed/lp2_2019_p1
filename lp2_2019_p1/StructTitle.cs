using System;
using System.Collections.Generic;
using System.Text;

namespace lp2_2019_p1
{
    public struct StructTitle
    {
        // Type of title.
        public string TitleType { get; }

        // Primary name of the title.
        public string PrimaryTitle { get; }

        // If its for adults or not.
        public bool ForAdults { get; }

        // The start year (if it exists).
        public short? StartYear { get; }

        // The end year (if it exists).
        public short? EndYear { get; }

        // Title's genres.
        public IEnumerable<string> Genres { get; }

        public StructTitle(
            string titleType, string primaryTitle, bool forAdults,
            short? startYear, short? endYear, IEnumerable<string> genres)
        {
            TitleType = titleType;
            PrimaryTitle = primaryTitle;
            ForAdults = forAdults;
            StartYear = startYear;
            EndYear = endYear;
            Genres = genres;
        }
    }
}
