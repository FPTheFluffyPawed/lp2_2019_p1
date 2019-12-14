using System;
using System.Collections.Generic;
using System.Text;

namespace lp2_2019_p1
{
    public struct StructTitleTotal
    {
        // Unique identifier.
        public string RTitleIdentifier { get; set; }

        // Type of title.
        public string RTitleType { get; set; }

        // Primary name of the title.
        public string RPrimaryTitle { get; set; }

        // If its for adults or not.
        public bool RForAdults { get; set; }

        // The start year (if it exists).
        public short? RStartYear { get; set; }

        // The end year (if it exists).
        public short? REndYear { get; set; }

        public float RRatingsAverage { get; set; }

        // Title's genres.
        public IEnumerable<string> RGenres { get; set; }

        public StructTitleTotal(
            string titleIdentifier_, string titleType_, string primaryTitle_,
            bool forAdults_, short? startYear_, short? endYear_,
            float ratingsAverage_, IEnumerable<string> genres_)
        {
            RTitleIdentifier = titleIdentifier_;
            RTitleType = titleType_;
            RPrimaryTitle = primaryTitle_;
            RForAdults = forAdults_;
            RStartYear = startYear_;
            REndYear = endYear_;
            RRatingsAverage = ratingsAverage_;
            RGenres = genres_;
        }
    }
}
