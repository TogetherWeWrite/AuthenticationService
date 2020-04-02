using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Interfaces
{
    public interface IAuthenticationRepository: IRegister, ILogin
    {
        void ValidateSession(Object sessionToken);
    }

    public interface IRegister
    {
        /// <summary>
        /// Method used for Registering a new account with username and password
        /// </summary>
        /// <param name="username">Username is the name the user will go by on the application.</param>
        /// <param name="password">The password the user</param>
        /// <exception cref="FormatException"> When the formatting of the username or password are not correct this exception will be thrown</exception>
        /// <returns>boolean that will indicate if the Register has been succesfully been done</returns>
        bool RegisterAccount(string username, string password);
    }

    public interface ILogin
    {
        bool Login(string username, string password);
    }
}
