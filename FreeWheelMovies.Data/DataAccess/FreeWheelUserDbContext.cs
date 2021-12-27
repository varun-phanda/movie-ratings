using FreeWheelMovies.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace FreeWheelMovies.Data
{
    public class FreeWheelUserDbContext : DbContext
    {
        public FreeWheelUserDbContext(DbContextOptions<FreeWheelUserDbContext> options)
            : base(options)
        { }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(usr => new { usr.ID });
            modelBuilder.Entity<User>().HasQueryFilter(usr => usr.IsActive);
        }
    }
}
