using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Obligatorisk3.Models
{
    public class User
    {
        public string userName { get; private set; }
        public int userId { get; private set; }
        public bool isAdmin { get; private set; }

        public User(string userName, int userId, bool isAdmin)
        {
            this.userName = userName;
            this.userId = userId;
            this.isAdmin = isAdmin;
        }
    }
}