using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.View
{
    public class ViewUser
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }

    public static class ExtensionAccount
    {
        public static ViewUser WithoutPassword(this ViewUser user)
        {
            user.Password = null;
            return user;
        }
    }

    public class SessionToken
    {
        public string Username { get; set; }
        public object sessionToken { get; set; }
    }


}
