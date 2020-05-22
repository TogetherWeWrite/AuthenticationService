using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Publishers
{
    public interface IUserPublisher
    {
        Task PublishRegisterUser(Guid id, string username);
        Task PublishDeleteuser(Guid id);
        Task PublishChangeName(Guid id, string newUsername);
    }
}
