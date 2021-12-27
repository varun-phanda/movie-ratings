using System.Collections.Generic;
using System.Threading.Tasks;
using FreeWheelMovies.Shared.Entities;

namespace FreeWheelMovies.Data.DataManager.Interfaces
{
    public interface IMovieRatingDataManager
    {
        IEnumerable<MovieRating> GetAllMovieRatings();
        Task<bool> SaveMovieRatingAsync(MovieRating review);
        double GetMovieAverageRating(int movieId);
        Task<List<MovieRating>> GetTopMovieRatingsByUserAsync(int userID);
        Task<List<MovieRating>> GetTopRatedMoviesAsync(int noOfRecords);
    }
}