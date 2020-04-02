using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using AuthenticationService.Interfaces;
using AuthenticationService.Data;
using AuthenticationService.Models;
using AuthenticationService.Services;
using AuthenticationService.Exceptions;
using System.Reflection;

namespace AuthenticationService.Logic
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private IEncryptionService _encryptionService;
        private AuthenticationContext _authenticationContext;
        public AuthenticationRepository(AuthenticationContext _authenticationContext, IEncryptionService _encryptionService)
        {
            this._authenticationContext = _authenticationContext;
            this._encryptionService = _encryptionService;
        }
        public bool Login(string username, string password)
        {
            username.IsStringNotNullOrEmpty("Username");
            password.IsStringNotNullOrEmpty("Password");
            try
            {
                var account = _authenticationContext.Accounts.Single(x => x.Username == username);
                if (_encryptionService.VerifyHash(password, account.Salt, account.Password))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (ArgumentNullException)
            {
                throw new InvalidLoginException("It Appears that no account with this username is registered");
            }
            catch (InvalidOperationException)
            {
                throw new InvalidLoginException("It appears due to spaghetti code that your username is also used by another user, the admin has been notified and you will get a message shortly with more information. Our applogies.");
            }
            

        }

        public bool RegisterAccount(string username, string password)
        {
            username.IsStringNotNullOrEmpty("Username");
            password.IsStringNotNullOrEmpty("Password");
            var salt = _encryptionService.GenerateSalt();
            var encryptedpassword = _encryptionService.EncryptWord(password, salt);
            if(_authenticationContext.Accounts.Select(account => account.Username == username).Count() > 0)
            {
                throw new UsernameAlreadyTakenException("There is already an account with this username");
            }
            _authenticationContext.Accounts.Add(new Account()
            {
                Username = username,
                Password = encryptedpassword,
                Salt = salt

            });
            _authenticationContext.SaveChanges();
            return true;
        }

        public void ValidateSession(object sessionToken)
        {
            throw new NotImplementedException();
        }
    }
}
