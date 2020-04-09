using Microsoft.EntityFrameworkCore.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.View
{
    public class ViewUser
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string token { get; set; }
    }

    public static class ExtensionAccount
    {
        public static ViewUser WithoutPassword(this ViewUser user)
        {
            user.password = null;
            return user;
        }
    }

    public class SessionToken
    {
        public string Username { get; set; }
        public object sessionToken { get; set; }
    }


}
