using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly DataContext _context;
        public ConversationRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<Conversation> CreateMess(string user, string user_counter, string mess)
        {
            Conversation messChat = new Conversation();
            messChat.UserName = user;
            messChat.UserName_Counter = user_counter;
            messChat.TimeChat = DateTime.Now.ToString("dd-MM-yyyy");
            messChat.Mess = mess;
            await _context.Conversations.AddAsync(messChat);
            await _context.SaveChangesAsync();
            return messChat;
        }

        public List<Conversation> GetConversation(string user, string user_counter)
        {
            var conversations = _context.Conversations.Where(s => s.UserName == user && s.UserName_Counter == user_counter).ToList();
            return conversations;
        }
    }
}