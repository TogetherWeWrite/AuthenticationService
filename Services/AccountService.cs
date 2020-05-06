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
            return account.toViewAccount();
        }
    }
}
