using FreeWheelMovies.Business.Interfaces;
using FreeWheelMovies.Data.DataManager.Interfaces;
using FreeWheelMovies.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeWheelMovies.Business
{
    public class MovieService : IMovieService
    {
        private readonly IMovieDataManager dataManager;
        
        public MovieService(IMovieDataManager dm)
        {
            this.dataManager = dm;
        }

        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            return await dataManager.GetAllMovies();
        }

        public Movie GetMovie(int id)
        {
            var movie = dataManager.GetMovie(id);
            return movie;
        }

        public async Task<bool> SaveMovieAsync(Movie movie)
        {
            return await dataManager.SaveMovieAsync(movie);
        }

        public async Task<List<Movie>> SearchMoviesAsync(string title, string yearOfRelease, string genre)
        {
            //Validate
            if (string.IsNullOrEmpty(title)
                && string.IsNullOrEmpty(yearOfRelease)
                && string.IsNullOrEmpty(genre))
            {
                throw new ArgumentException("Invalid - No criteria provided");
            }

            return await dataManager.SearchMoviesAsync(title, yearOfRelease, genre);
        }
    }
}
