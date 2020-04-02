using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Exceptions;

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
        /// <exception cref="UsernameAlreadyTakenException"> When the formatting of the username or password are not correct this exception will be thrown</exception>
        /// <returns>boolean that will indicate if the Register has been succesfully been done</returns>
        bool RegisterAccount(string username, string password);
    }

    public interface ILogin
    {
        /// <summary>
        /// Method used for logging in will run your password through argon 2 technique with the salt that is found with your username
        /// </summary>
        /// <param name="username">username that will help find the salt</param>
        /// <param name="password">the password that will generate the same hash if it uses the right salt assuming your password is correct</param>
        /// <returns>bool which indicates if the combination of username and password is correct</returns>
        /// <exception cref="InvalidLoginException">When there is no account with this username</exception>
        /// <exception cref="InvalidLoginException">When there is are multiple accounts with this username which shouldnt be possible</exception>
        bool Login(string username, string password);
    }
}
