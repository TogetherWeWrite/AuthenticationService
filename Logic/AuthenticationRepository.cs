using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Interfaces;

namespace AuthenticationService.Logic
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        public void Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool RegisterAccount(string username, string password)
        {
            return true;
        }

        public void ValidateSession(object sessionToken)
        {
            throw new NotImplementedException();
        }
    }
}
