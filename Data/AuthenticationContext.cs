using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Data
{
    public class AuthenticationContext : DbContext
    {
        
        public AuthenticationContext(DbContextOptions options) : base (options){}

        

        public DbSet<Account> Accounts { get; set; }
    }
}
