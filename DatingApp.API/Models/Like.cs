namespace DatingApp.API.Models
{
    public class Like
    {
        public int LikerId { get; set; }
        public int LikeeId { get; set; }
        // nguoi like
        public User Liker { get; set; }
        // so like
        public User Likee { get; set; }
    }
}