using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Models;

namespace AuthenticationService.Interfaces
{
    public interface IAccountRepository
    {
        /// <summary>
        /// Gets a list of all the users
        /// </summary>
        /// <returns>List of all the users</returns>
        List<Account> Get();

        /// <summary>
        /// Gets a single user by their username
        /// </summary>
        /// <param name="username">The username to search for</param>
        /// <returns>User with the correct username</returns>
        Account Get(string username);

        /// <summary>
        /// Gets a single user by their Guid
        /// </summary>
        /// <param name="id">The guid to search for</param>
        /// <returns>User with the correct guid</returns>
        Account Get(int id);

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="user">User to be saved</param>
        /// <returns>Created user</returns>
        Account Create(Account user);

        /// <summary>
        /// Updates an existing user
        /// </summary>
        /// <param name="id">Guid of the user</param>
        /// <param name="userIn">User with updated fields</param>
        /// <returns>Updated user</returns>
        Account Update(int id, Account userIn);

        /// <summary>
        /// Removes an user
        /// </summary>
        /// <param name="userIn">User to remove</param>
        void Remove(int id);
    }
}
