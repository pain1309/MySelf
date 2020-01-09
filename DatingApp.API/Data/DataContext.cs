using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<NewsItemEntity> NewsItemEntities { get; set; }
        public DbSet<NewsGroup> NewsGroups { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
    }
}