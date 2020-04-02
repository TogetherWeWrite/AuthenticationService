using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Exceptions
{
    public class InvalidLoginException : Exception
    {
        public InvalidLoginException(string message) : base(message)
        {

        }

    }

    public class UsernameAlreadyTakenException : Exception
    {
        public UsernameAlreadyTakenException(string message) : base(message)
        {

        }

    }
}
