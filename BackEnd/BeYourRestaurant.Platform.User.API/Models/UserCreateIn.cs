using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeYourRestaurant.Platform.User.API.Models
{
    public class UserCreateIn
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
