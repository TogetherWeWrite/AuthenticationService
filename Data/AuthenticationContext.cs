using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using AuthenticationService.Models;

namespace AuthenticationService.Data
{
    public class AuthenticationContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
    }
}
