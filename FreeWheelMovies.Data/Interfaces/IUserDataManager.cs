using System.Collections.Generic;
using FreeWheelMovies.Shared.Entities;

namespace FreeWheelMovies.Data.DataManager
{
    public interface IUserDataManager
    {
        IEnumerable<User> GetAllUsers();
        User GetUser(int id);
    }
}