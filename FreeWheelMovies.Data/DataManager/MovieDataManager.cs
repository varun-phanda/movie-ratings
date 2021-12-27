using FreeWheelMovies.Data.DataManager.Interfaces;
using FreeWheelMovies.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeWheelMovies.Data.DataManager
{
    public class MovieDataManager : IMovieDataManager
    {
        private readonly FreeWheelMovieDbContext db;

        public MovieDataManager(FreeWheelMovieDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Movie>> GetAllMovies()
        {
            var mv = db.Set<Movie>();
            IQueryable<Movie> movies = null;
            if (mv.Count() > 0) {
                movies = mv.OrderBy(p => p.Title);
            }
            return await movies.ToListAsync();
        }
        
        public async Task<List<Movie>> SearchMoviesAsync(string title, string yearOfRelease, string genre)
        {
            var movies = db.Set<Movie>().Where(mv => mv.IsActive);

            if (!string.IsNullOrWhiteSpace(title))
            {
                movies = movies.Where(m => m.Title.Contains(title));
            }

            if (!string.IsNullOrWhiteSpace(yearOfRelease))
            {
                int yearRelease = 0;
                if(Int32.TryParse(yearOfRelease, out yearRelease))
                {
                    movies = movies.Where(m => m.ReleaseDate.Year == yearRelease);
                }
            }

            if (!string.IsNullOrWhiteSpace(genre))
            {
                movies = movies.Where(m => m.Genre.Contains(genre));
            }

            return await movies.ToListAsync();
        }

        public async Task<bool> SaveMovieAsync(Movie movie)
        {
            var movieToAdd = db.Set<Movie>().FirstOrDefault(p => p.ID == movie.ID);
            
            if (movieToAdd != null)
            {
                movieToAdd.Title = movie.Title;
                movieToAdd.Description = movie.Description;
                movieToAdd.Genre = movie.Genre;
                movieToAdd.ReleaseDate = movie.ReleaseDate;
                db.Set<Movie>().Update(movieToAdd);
            }
            else
            {
                movieToAdd = new Movie
                {
                    Title = movie.Title,
                    Description = movie.Description,
                    Genre = movie.Genre,
                    ReleaseDate = movie.ReleaseDate,
                    IsActive = true
                };
                db.Set<Movie>().Add(movieToAdd);
            }

            return await db.SaveChangesAsync() > 0;
        }

        public Movie GetMovie(int id)
        {
            var movie = db.Set<Movie>().FirstOrDefault(p => p.ID == id);
            return movie;
        }

        public async Task<bool> UpdateMovieAverageRatingAsync(int movieID, double averageRating)
        {
            var movie = db.Set<Movie>().FirstOrDefault(p => p.ID == movieID);
            if (movie != null)
            {
                movie.AverageRating = averageRating;
                db.Set<Movie>().Update(movie);
            }
            else
            {
                return false;
            }

            return await db.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="noOfRecords"></param>
        /// <returns></returns>
        public async Task<List<Movie>> GetTopMoviesAsync(int noOfRecords)
        {
            var topMovies = db.Set<Movie>().OrderByDescending(mv => mv.AverageRating).ThenBy(mv => mv.Title).Take(noOfRecords);
            return await topMovies.ToListAsync();
        }
    }
}
