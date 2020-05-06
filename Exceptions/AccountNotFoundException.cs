using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Exceptions
{
    public class AccountNotFoundException : Exception
    {
        public AccountNotFoundException(string msg) : base(msg)
        {
        
        }
    }
}
