using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Models
{
    public class User
    {



        public long Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public User(string username, string pass, string role)
        {
            UserName = username;
            Password = pass;
            Role = role;

        }
        public User() { }

    }
}
