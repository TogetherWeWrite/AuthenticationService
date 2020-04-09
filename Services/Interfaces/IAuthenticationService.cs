using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Exceptions;
using AuthenticationService.View;

namespace AuthenticationService.Services.Interfaces
{
    public interface IAuthenticationService: IRegister, ILogin
    {
        
    }
}
