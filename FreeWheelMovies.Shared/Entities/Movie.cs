using System;

namespace FreeWheelMovies.Shared.Entities
{
    public class Movie
    {
        public int? ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public double? AverageRating { get; set; }
        public bool IsActive { get; set; }
    }
}
