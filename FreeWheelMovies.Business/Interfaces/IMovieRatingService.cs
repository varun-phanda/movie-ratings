using FreeWheelMovies.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeWheelMovies.Business.Interfaces
{
    public interface IMovieRatingService
    {
        Task<double> GetMovieRating(int movieID);

        Task<bool> SaveMovieReviewAsync(MovieRating review);
        Task<List<Movie>> GetTopRatedMoviesByUserAsync(int userID, int numberOfTopMovies);
        Task<List<Movie>> GetTopRatedMoviesAsync(int noOfRecords);
    }
}
