using AuthenticationService.Data;
using AuthenticationService.Models;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Interfaces
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AuthenticationContext _db;
        public AccountRepository(AuthenticationContext db)
        {
            this._db = db;
        }

        public Account Create(Account user)
        {
            _db.Accounts.Add(user);
            _db.SaveChanges();
            return Get(user.Username);
        }

        public List<Account> Get()
        {
            return _db.Accounts.ToList();
        }

        public Account Get(string username)
        {
            return _db.Accounts.SingleOrDefault(account => account.Username == username);
        }

        public Account Get(int id)
        {
            return _db.Accounts.SingleOrDefault(account => account.Id == id);
        }

        public void Remove(int id)
        {
            _db.Accounts.Remove(Get(id));
            _db.SaveChanges();
        }

        public Account Update(int id, Account toUpdateTo)
        {
            var account = Get(id);
            account.TakeOverVariables(toUpdateTo);
            _db.SaveChanges();
            return account;
        }
    }
}
