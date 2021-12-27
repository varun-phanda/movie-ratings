using System.Collections.Generic;
using System.Threading.Tasks;
using FreeWheelMovies.Shared.Entities;

namespace FreeWheelMovies.Data.DataManager.Interfaces
{
    public interface IMovieDataManager
    {
        Task<List<Movie>> GetAllMovies();
        Movie GetMovie(int id);
        Task<bool> SaveMovieAsync(Movie movie);
        Task<List<Movie>> SearchMoviesAsync(string title, string yearOfRelease, string genre);
        Task<bool> UpdateMovieAverageRatingAsync(int movieID, double averageRating);
        Task<List<Movie>> GetTopMoviesAsync(int noOfRecords);
    }
}