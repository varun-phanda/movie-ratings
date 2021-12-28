using FreeWheelMovies.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace FreeWheelMovies.Data
{
    public class FreeWheelMovieRatingDbContext : DbContext
    {
        public FreeWheelMovieRatingDbContext(DbContextOptions<FreeWheelMovieRatingDbContext> options)
            : base(options)
        { }

        public DbSet<MovieRating> MovieRating { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieRating>().HasKey(mv => new { mv.ID });
            modelBuilder.Entity<MovieRating>().HasQueryFilter(mv => mv.IsActive);
        }
    }
}
