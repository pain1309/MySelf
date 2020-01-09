using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    public interface IConversationRepository
    {
        Task<Conversation> CreateMess(string user, string user_counter, string mess);
        List<Conversation> GetConversation(string user, string user_counter);
    }
}