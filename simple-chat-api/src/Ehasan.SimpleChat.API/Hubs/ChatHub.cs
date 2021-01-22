using Ehasan.SimpleChat.Core.Business_Interface;
using Ehasan.SimpleChat.Core.Entities;
using Ehasan.SimpleChat.Core.Model;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ehasan.SimpleChat.API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessageService messageService;
        public ChatHub(IMessageService messageService)
        {
            this.messageService = messageService;
        }
        static IList<UserConnection> Users = new List<UserConnection>();

        public class UserConnection
        {
            public string UserId { get; set; }
            public string ConnectionId { get; set; }
            public string FullName { get; set; }
            public string Username { get; set; }
        }

        public Task SendMessageToUser(Message message)
        {
            var reciever = Users.FirstOrDefault(x => x.UserId == message.Receiver);
            var connectionId = reciever == null ? "offlineUser" : reciever.ConnectionId;
            this.messageService.Add(message);
            return Clients.Client(connectionId).SendAsync("ReceiveDM", Context.ConnectionId, message);
        }

        public async Task DeleteMessage(MessageDeleteModel message)
        {
            var deletedMessage=await this.messageService.DeleteMessage(message);
            await Clients.All.SendAsync("BroadCastDeleteMessage", Context.ConnectionId, deletedMessage);
        }

        public async Task PublishUserOnConnect(string id, string fullname, string username)
        {

            var existingUser = Users.FirstOrDefault(x => x.Username == username);
            var indexExistingUser = Users.IndexOf(existingUser);

            UserConnection user = new UserConnection
            {
                UserId = id,
                ConnectionId = Context.ConnectionId,
                FullName = fullname,
                Username = username
            };

            if (!Users.Contains(existingUser))
            {
                Users.Add(user);

            }
            else
            {
                Users[indexExistingUser] = user;
            }

            await Clients.All.SendAsync("BroadcastUserOnConnect", Users);

        }

        public void RemoveOnlineUser(string userID)
        {
            var user = Users.Where(x => x.UserId == userID).ToList();
            foreach (UserConnection i in user)
                Users.Remove(i);

            Clients.All.SendAsync("BroadcastUserOnDisconnect", Users);
        }
    }
}
