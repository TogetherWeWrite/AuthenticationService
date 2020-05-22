using AuthenticationService.Exceptions;
using AuthenticationService.Interfaces;
using AuthenticationService.Models;
using AuthenticationService.Publishers;
using AuthenticationService.Setttings;
using AuthenticationService.View;
using MessageBroker;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
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
        private readonly IMessageQueuePublisher _messageQueuePublisher;
        private readonly MessageQueueSettings _messageQueueSettings;
        private readonly IUserPublisher _userPublisher;
        public AuthenticationServices(IEncryptionService _encryptionService, ITokenService _tokenservice, IAccountRepository _accountRepository
            , IUserPublisher publisher)
        {
            this._encryptionService = _encryptionService;
            this._tokenservice = _tokenservice;
            this._accountRepository = _accountRepository;
            this._userPublisher = publisher;
        }

        public async Task<ViewUser> Login(string username, string password)
        {
            username.IsStringNotNullOrEmpty("Username");
            password.IsStringNotNullOrEmpty("Password");
            try
            {
                var account = await _accountRepository.Get(username);
                if (account == null)
                {
                    throw new InvalidLoginException("No account with the username: " + username);
                }
                if (_encryptionService.VerifyHash(password, account.Salt, account.Password))
                {
                    var accountWithtoken = _tokenservice.Authenticate(account);
                    await _accountRepository.Update(account.Id, accountWithtoken);
                    return new View.ViewUser()
                    {
                        Id = Convert.ToString(account.Id),
                        Username = account.Username,
                        Token = account.Token
                    };
                }
                else
                {
                    throw new InvalidLoginException("password is incorrect");
                }
            }
            catch (NullReferenceException ex)
            {
                throw new InvalidLoginException("no user found");
            }
        }

        public async Task<bool> RegisterAccount(string username, string password)
        {
            username.IsStringNotNullOrEmpty("Username");
            password.IsStringNotNullOrEmpty("Password");
            var salt = _encryptionService.GenerateSalt();
            var encryptedpassword = _encryptionService.EncryptWord(password, salt);
            if (await _accountRepository.Get(username) != null)
            {
                throw new UsernameAlreadyTakenException("There is already an account with this username");
            }
            var newaccount = await _accountRepository.Create(new Account()
            {
                Username = username,
                Password = encryptedpassword,
                Salt = salt

            });
            await _userPublisher.PublishRegisterUser(newaccount.Id, newaccount.Username);
            return true;
        }

        public async Task<bool> ValidateToken(string username, string token)
        {
            try
            {
                //TODO: Use tokenservice to check if token is still valid.
                var acc = await _accountRepository.Get(username);
                return token == acc.Token;//if token is same return true if not false
            }
            catch (NullReferenceException)
            {
                throw new InvalidLoginException("no user found with this username");
            }
        }
    }
}
