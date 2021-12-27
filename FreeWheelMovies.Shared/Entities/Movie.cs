using System;
using System.Runtime.Serialization;

namespace FreeWheelMovies.Shared.Entities
{
    public class Movie : FreeWheelBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public double? AverageRating { get; set; }
    }
}
