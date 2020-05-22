using AuthenticationService.Setttings;
using MessageBroker;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Publishers
{
    public class UserPublisher : IUserPublisher
    {
        private readonly IMessageQueuePublisher _messageQueuePublisher;
        private readonly MessageQueueSettings _messageQueueSettings;
        public UserPublisher(IMessageQueuePublisher messageQueuePublisher, IOptions<MessageQueueSettings> messageQueueSettings)
        {
            this._messageQueuePublisher = messageQueuePublisher;
            this._messageQueueSettings = messageQueueSettings.Value;
        }

        public async Task PublishChangeName(Guid id, string newUsername)
        {
            await _messageQueuePublisher.PublishMessageAsync(
                _messageQueueSettings.Exchange
                , "authentication-service"
                , "change-username"
                , new
                {
                    id,
                    newUsername,
                }
                );
        }

        public async Task PublishDeleteuser(Guid id)
        {
            await _messageQueuePublisher.PublishMessageAsync(
                   _messageQueueSettings.Exchange
                   , "authentication-service"
                   , "delete-user"
                   , new
                   {
                       id,
                   }
                   );
        }

        public async Task PublishRegisterUser(Guid id, string username)
        {
            await _messageQueuePublisher.PublishMessageAsync(
                _messageQueueSettings.Exchange
                , "authentication-service"
                , "register-new-user"
                , new
                {
                    id,
                    username,
                }
                );
        } 
    }
}
