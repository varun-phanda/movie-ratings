using System;

namespace FreeWheelMovies.Shared.Entities
{
    public class User
    {
        public int? ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public bool IsActive { get; set; }
    }
}
