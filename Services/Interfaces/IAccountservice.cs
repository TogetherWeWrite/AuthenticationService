using AuthenticationService.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Services.Interfaces
{
    public interface IAccountservice
    {
        Task<ViewAccount> GetAccount(Guid id);
        Task<Guid> GetId(string username);
    }
}
