using System;
using System.Collections.Generic;
using System.Text;

namespace lp2_2019_p1
{
    /// <summary>
    /// Struct class used to store our ratings information obtained from
    /// the 'titles.ratings' file.
    /// </summary>
    public struct StructRatings
    {
        // Unique identifier.
        public string RatingsIdentifier { get; }

        // Rating given to the title.
        public float RatingsAverage { get; }

        public StructRatings(string ratingsIdentifier, float ratingsAverage)
        {
            RatingsIdentifier = ratingsIdentifier;
            RatingsAverage = ratingsAverage;
        }
    }
}
