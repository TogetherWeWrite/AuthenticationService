using AuthenticationService.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Services.Interfaces
{
    public interface ILogin : IValidateToken
    {
        /// <summary>
        /// Method used for logging in will run your password through argon 2 technique with the salt that is found with your username, if succesful will return model without password.
        /// </summary>
        /// <param name="username">username that will help find the salt</param>
        /// <param name="password">the password that will generate the same hash if it uses the right salt assuming your password is correct</param>
        /// <returns><see cref="ViewUser"/> Which contains a token and **not** the password</returns>
        /// <exception cref="InvalidLoginException">When there is no account with this username</exception>
        /// <exception cref="InvalidLoginException">When there is are multiple accounts with this username which shouldnt be possible</exception>
        Task<ViewUser> Login(string username, string password);
    }

}
