using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace AuthenticationService.Models
{
    public class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
