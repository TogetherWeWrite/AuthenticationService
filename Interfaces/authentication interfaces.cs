using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Exceptions;
using AuthenticationService.View;

namespace AuthenticationService.Interfaces
{
    public interface IAuthenticationRepository: IRegister, ILogin
    {
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

    public interface ILogin : IValidateToken
    {
        /// <summary>
        /// Method used for logging in will run your password through argon 2 technique with the salt that is found with your username, if succesful will return model without password.
        /// </summary>
        /// <param name="username">username that will help find the salt</param>
        /// <param name="password">the password that will generate the same hash if it uses the right salt assuming your password is correct</param>
        /// <returns><see cref="Account"/> Which contains a token and **not** the password</returns>
        /// <exception cref="InvalidLoginException">When there is no account with this username</exception>
        /// <exception cref="InvalidLoginException">When there is are multiple accounts with this username which shouldnt be possible</exception>
        Account Login(string username, string password);
    }

    public interface IValidateToken
    {
        /// <summary>
        /// Method used for validating a previousvly generated token
        /// </summary>
        /// <param name="username">the username of the account that uses the token</param>
        /// <param name="token">The token you want to validate</param>
        /// <returns>bool which indicates if token is correctly or not.</returns>
        /// <exception cref="InvalidLoginException"></exception>
        bool ValidateToken(string username, string token);
    }
}
