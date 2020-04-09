using AuthenticationService.Exceptions;
using AuthenticationService.Interfaces;
using AuthenticationService.Models;
using AuthenticationService.View;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Services
{
    public class AuthenticationServices : Interfaces.IAuthenticationService
    {
        private readonly IEncryptionService _encryptionService;
        private readonly ITokenService _tokenservice;
        private readonly IAccountRepository _accountRepository;
        public AuthenticationServices(IEncryptionService _encryptionService, ITokenService _tokenservice, IAccountRepository _accountRepository)
        {
            this._encryptionService = _encryptionService;
            this._tokenservice = _tokenservice;
            this._accountRepository = _accountRepository;
        }
        public ViewUser Login(string username, string password)
        {
            username.IsStringNotNullOrEmpty("Username");
            password.IsStringNotNullOrEmpty("Password");
            try
            {
                var account = _accountRepository.Get(username);
                if (_encryptionService.VerifyHash(password, account.Salt, account.Password))
                {
                    var accountWithtoken = _tokenservice.Authenticate(account);
                    _accountRepository.Update(account.Id, accountWithtoken);
                    return new View.ViewUser()
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
            catch(NullReferenceException ex)
            {
                throw new InvalidLoginException("no user found");
            }
        }

        public bool RegisterAccount(string username, string password)
        {
            username.IsStringNotNullOrEmpty("Username");
            password.IsStringNotNullOrEmpty("Password");
            var salt = _encryptionService.GenerateSalt();
            var encryptedpassword = _encryptionService.EncryptWord(password, salt);
            if(_accountRepository.Get(username) !=null)
            {             
                throw new UsernameAlreadyTakenException("There is already an account with this username");
            }
            _accountRepository.Create(new Account()
            {
                Username = username,
                Password = encryptedpassword,
                Salt = salt

            });
            return true;
        }

        public bool ValidateToken(string username, string token)
        {
            try
            {
                //TODO: Use tokenservice to check if token is still valid.
                return token ==  _accountRepository.Get(username).Token;//if token is same return true if not false
            }
            catch (NullReferenceException)
            {
                throw new InvalidLoginException("no user found with this username");
            }
        }
    }
}
