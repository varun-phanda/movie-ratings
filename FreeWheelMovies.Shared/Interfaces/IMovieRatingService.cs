using FreeWheelMovies.Shared.Entities;
using System.Threading.Tasks;

namespace FreeWheelMovies.Shared.Interfaces
{
    public interface IMovieRatingService
    {
        Task<bool> SaveMovieReviewAsync(MovieRating review);
    }
}
