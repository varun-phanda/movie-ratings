
using FreeWheelMovies.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeWheelMovies.Shared.Interfaces
{
    public interface IMovieService
    {
        IEnumerable<Movie> GetAllMovies();

        Movie GetMovie(int id);

        Task<bool> SaveMovieAsync(Movie movie);

        Task<List<Movie>> SearchMoviesAsync(string title, string yearOfRelease, string genre);
    }
}
