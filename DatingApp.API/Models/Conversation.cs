using System;

namespace DatingApp.API.Models
{
    public class Conversation
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserName_Counter { get; set; }
        public string TimeChat { get; set; }
        public string Mess { get; set; }
    }
}