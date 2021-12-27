using System;
using System.Runtime.Serialization;

namespace FreeWheelMovies.Shared.Entities
{
    /// <summary>
    /// Movie Rating
    /// </summary>
    public class MovieRating : FreeWheelBase
    {
        /// <summary>
        /// User ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// Movie ID
        /// </summary>
        public int MovieID { get; set; }
        /// <summary>
        /// Rating in range of 1-5
        /// </summary>
        public RatingStars? Rating { get; set; }
        /// <summary>
        /// Modified at DateTime
        /// </summary>
        [IgnoreDataMember]
        public DateTime ModifiedAt { get; set; }
        /// <summary>
        /// Comment about the Movie
        /// </summary>
        public string Comment { get; set; }
    }

    public enum RatingStars
    {
        One = 1,
        Two,
        Three,
        Four,
        Five
    }
}
