using FreeWheelMovies.Data.DataManager.Interfaces;
using FreeWheelMovies.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeWheelMovies.Data.DataManager
{
    public class MovieRatingDataManager : IMovieRatingDataManager
    {
        private readonly FreeWheelMovieRatingDbContext db;

        public MovieRatingDataManager(FreeWheelMovieRatingDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<MovieRating> GetAllMovieRatings()
        {
            var reviewsDbSet = db.Set<MovieRating>();
            IEnumerable<MovieRating> reviews = null;
            if (reviewsDbSet.Count() > 0) {
                reviews = reviewsDbSet.OrderBy(p => p.ModifiedAt).ToList();
            }
            return reviews;
        }
        
        /// <summary>
        /// Get Average rating for a movie
        /// </summary>
        /// <param name="movieId">movie Id</param>
        /// <returns>Returns the rating rounded to closest 0.5</returns>
        public double GetMovieAverageRating(int movieId)
        {
            double average = 0;
            var movieRatings = db.Set<MovieRating>().Where(mvr => mvr.MovieID == movieId);
            if (movieRatings.Count() > 0)
            {
                average = movieRatings.Average(mvr => (int)mvr.Rating);
            }
            return Math.Round(average * 2, MidpointRounding.AwayFromZero) / 2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rating"></param>
        /// <returns></returns>
        public async Task<bool> SaveMovieRatingAsync(MovieRating rating)
        {
            var movieRating = db.Set<MovieRating>().FirstOrDefault(p => p.UserID == rating.UserID && p.MovieID == rating.MovieID);
            if(movieRating == null)
            {
                movieRating = new MovieRating
                {
                    MovieID = rating.MovieID,
                    UserID = rating.UserID,
                    Rating = rating.Rating,
                    Comment = rating.Comment,
                    ModifiedAt = DateTime.Now,
                    IsActive = true
            };
                db.Set<MovieRating>().Add(movieRating);
            }
            else
            {
                movieRating.Rating = rating.Rating;
                movieRating.Comment = rating.Comment;
                movieRating.ModifiedAt = DateTime.Now;
                movieRating.IsActive = true;
                db.Set<MovieRating>().Update(movieRating);
            }
            return await db.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<List<MovieRating>> GetTopRatedMoviesAsync(int noOfRecords)
        {
            List<Movie> movies = new List<Movie>();

            var userMovieRatings = db.Set<MovieRating>().OrderByDescending(mr => mr.Rating).Take(noOfRecords);
            return await userMovieRatings.ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<List<MovieRating>> GetTopMovieRatingsByUserAsync(int userID)
        {
            List<Movie> movies = new List<Movie>();

            var userMovieRatings = db.Set<MovieRating>().Where(mr => mr.UserID == userID).OrderByDescending(mr => mr.Rating); //.Take(5)
            return await userMovieRatings.ToListAsync();
        }
    }
}
