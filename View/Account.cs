using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.View
{
    public class Account
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class SessionToken
    {
        public string Username { get; set; }
        public object sessionToken { get; set; }
    }
}
