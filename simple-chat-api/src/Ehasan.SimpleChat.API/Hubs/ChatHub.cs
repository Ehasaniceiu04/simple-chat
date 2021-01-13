using Ehasan.SimpleChat.Core.Entities;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ehasan.SimpleChat.API.Hubs
{
    public class ChatHub : Hub
    {

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
            var connectionId = reciever == null ? "no one recive this message" : reciever.ConnectionId;
            return Clients.Client(connectionId).SendAsync("ReceiveDM", Context.ConnectionId, message);
        }

        public async Task OnConnect(string id, string fullname, string username)
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

            await Clients.All.SendAsync("OnConnect", Users);

        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("UserConnected", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public void RemoveOnlineUser(string userID)
        {
            var user = Users.Where(x => x.UserId == userID).ToList();
            foreach (UserConnection i in user)
                Users.Remove(i);

            Clients.All.SendAsync("OnDisconnect", Users);
        }
    }
}
