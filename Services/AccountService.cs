using AuthenticationService.Exceptions;
using AuthenticationService.Interfaces;
using AuthenticationService.Models;
using AuthenticationService.Services.Interfaces;
using AuthenticationService.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Services
{
    public class AccountService : IAccountservice
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            this._accountRepository = accountRepository;
        }
        public async Task<ViewAccount> GetAccount(int id)
        {
            var account = _accountRepository.Get(id);
            if (account == null)
            {
                throw new AccountNotFoundException("The account with the id: " + id + ", does not exist");
            }
            return account.toViewAccount();
        }

        public async Task<int> GetId(string username)
        {
            var account = _accountRepository.Get(username);
            if(account == null)
            {
                throw new AccountNotFoundException("The account with the username: " + username +", does not exist");
            }
            return account.Id;
        }
    }
}
