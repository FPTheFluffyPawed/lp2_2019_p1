using System;
using System.Collections.Generic;
using System.Text;

namespace lp2_2019_p1
{
    public struct StructRatings
    {
        // Unique identifier.
        public string RatingsIdentifier { get; }

        // Rating given to the title.
        public string RatingsAverage { get; }

        public StructRatings(string ratingsIdentifier, string ratingsAverage)
        {
            RatingsIdentifier = ratingsIdentifier;
            RatingsAverage = ratingsAverage;
        }
    }
}
