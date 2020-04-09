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
        private ITokenService _tokenservice;
        private AuthenticationContext _authenticationContext;
        public AuthenticationRepository(AuthenticationContext _authenticationContext, IEncryptionService _encryptionService, ITokenService _tokenservice)
        {
            this._authenticationContext = _authenticationContext;
            this._encryptionService = _encryptionService;
            this._tokenservice = _tokenservice;
        }

        public View.Account Login(string username, string password)
        {
            username.IsStringNotNullOrEmpty("Username");
            password.IsStringNotNullOrEmpty("Password");
            try
            {
                var account = _authenticationContext.Accounts.Single(x => x.Username == username);
                if (_encryptionService.VerifyHash(password, account.Salt, account.Password))
                {
                    var accountWithtoken = _tokenservice.Authenticate(account);
                    _authenticationContext.SaveChanges();
                    return new View.Account()
                    {
                        id = account.Id,
                        username = account.Username,
                        token = account.Token
                    };
                }
                else
                {
                    throw new InvalidLoginException("password is incorrect");
                }
            }
            catch (ArgumentNullException)
            {
                //more than one result
                throw new InvalidLoginException("It appears due to spaghetti code that your username is also used by another user, the admin has been notified and you will get a message shortly with more information. Our applogies.");
            }
            catch (InvalidOperationException)
            {
                //no result
                throw new InvalidLoginException("It Appears that no account with this username is registered");
            }
            

        }

        public bool RegisterAccount(string username, string password)
        {
            username.IsStringNotNullOrEmpty("Username");
            password.IsStringNotNullOrEmpty("Password");
            var salt = _encryptionService.GenerateSalt();
            var encryptedpassword = _encryptionService.EncryptWord(password, salt);
            var x = _authenticationContext.Accounts.Select(account => account.Username == username);
            if (_authenticationContext.Accounts.SingleOrDefault(account => account.Username == username) != null)
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

        public bool ValidateToken(string username, string token)
        {
            try
            {
                //TODO: Use tokenservice to check if token is still valid.
                return token == _authenticationContext.Accounts.Single(x => x.Username == username).Token;//if token is same return true if not false
            }
            catch (InvalidOperationException)
            {
                //no result
                throw new InvalidLoginException("It Appears that no account with this username is registered");
            }
            catch (ArgumentNullException)
            {
                //more than one result
                throw new InvalidLoginException("It appears due to spaghetti code that your username is also used by another user, the admin has been notified and you will get a message shortly with more information. Our applogies.");
            }
        }
    }
}
