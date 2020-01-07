using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace DatingApp.API.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        #region---Data Members---
        static List<UserDetail> ConnectedUsers = new List<UserDetail>();
        static List<MessageDetail> CurrentMessage = new List<MessageDetail>();
        public override async Task OnConnectedAsync()
        {
            // await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            // await base.OnConnectedAsync();
            var id = Context.ConnectionId;
            if(ConnectedUsers.Count(x => x.ConnectionId == id) == 0) 
            {
                ConnectedUsers.Add(new UserDetail { ConnectionId = id, UserName = Context.User.Identity.Name + "-" + Context.UserIdentifier, UserID = Context.UserIdentifier})
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string message, string username)
        {
            await Clients.All.SendAsync("receive", username, message);
        }
    }
}