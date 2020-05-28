using AuthenticationService.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthenticationService;

namespace AuthenticationService.Interfaces
{
    public class AccountRepositoryMongo : IAccountRepository
    {
        private readonly IMongoCollection<Account> _users;
        public AccountRepositoryMongo(IAccountDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<Account>(settings.UserCollectionName);
        }
        public async Task<Account> Create(Account user)
        {
            await _users.InsertOneAsync(user);
            return user;
        }

        public async Task<Account> Get(Guid id)
        {
            return await _users.Find(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Account> Get(string Name)
        {
            return await _users.Find(user => user.Username == Name).FirstOrDefaultAsync();
        }

        public async Task Remove(Guid id)
        {
            await _users.DeleteOneAsync(user => user.Id == id);
        }

        public async Task<Account> Update(Guid id, Account update)
        {
            await _users.ReplaceOneAsync(user => user.Id == id, update);
            return update;
        }

       
    }
}
