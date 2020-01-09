using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace DatingApp.API.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IConversationRepository _repo;
        public ChatHub(IConversationRepository repo)
        {
            _repo = repo;
        }
        public override async Task OnConnectedAsync()
        {
            // await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            // await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string message, string user, string user_counter)
        {
            await Clients.All.SendAsync("receive", user, message);
            // user: nguoi chat
            // user_counter: nguoi chat cung
            await _repo.CreateMess(user, user_counter, message);
        }
    }
}