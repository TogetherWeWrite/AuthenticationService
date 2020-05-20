using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Services.Interfaces
{

    public interface IValidateToken
    {
        /// <summary>
        /// Method used for validating a previousvly generated token
        /// </summary>
        /// <param name="username">the username of the account that uses the token</param>
        /// <param name="token">The token you want to validate</param>
        /// <returns>bool which indicates if token is correctly or not.</returns>
        /// <exception cref="InvalidLoginException"></exception>
        Task<bool> ValidateToken(string username, string token);
    }

}
