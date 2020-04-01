using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Interfaces
{
    public interface IAuthenticationRepository
    {
        void RegisterAccount(string username, string password);
        void Login(string username, string password);
        void ValidateSession(Object sessionToken);
    }
}
