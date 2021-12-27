using System;

namespace FreeWheelMovies.Shared.Entities
{
    public class MovieRating
    {
        public int? ID { get; set; }
        public int UserID { get; set; }
        public int MovieID { get; set; }
        public RatingStars Rating { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string Comment { get; set; }
        public bool IsActive { get; set; }
    }

    public enum RatingStars
    {
        Default = 0,
        One = 1,
        Two,
        Three,
        Four,
        Five
    }
}
