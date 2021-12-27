using FreeWheelMovies.Data.DataManager.Interfaces;
using FreeWheelMovies.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeWheelMovies.Data.DataManager
{
    public class UserDataManager : IUserDataManager
    {
        private readonly FreeWheelUserDbContext db;

        public UserDataManager(FreeWheelUserDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<User> GetAllUsers()
        {
            var ur = db.Set<User>();
            IEnumerable<User> users = null;
            if (ur.Count() > 0) {
                users = ur.OrderBy(p => p.ID).ToList();
            }
            return users;
        }
        
        public User GetUser(int id)
        {
            var user = db.Set<User>().FirstOrDefault(p => p.ID == id);
            return user;
        }
    }
}
