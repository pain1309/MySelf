using System;

namespace DatingApp.API.Models
{
    public class Conversation
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserName_Counter { get; set; }
        public DateTime TimeChat { get; set; }
        public string Mess { get; set; }
    }
}