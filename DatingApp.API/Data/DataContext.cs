using DatingApp.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    // By inheriting from IdentityDbContext class we guarantee that Entity Framework (EF) Core 
    // will create all the necessary User-related tables in the database.
    public class DataContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, 
        UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserRole>(userRole => 
            {
                userRole.HasKey(ur => new {ur.UserId, ur.RoleId});
                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
            // builder.Entity<User>().Property(b => b.DateOfBirth).HasDefaultValueSql("getdate()");
            // builder.Entity<User>().Property(b => b.Created).HasDefaultValueSql("getdate()");
            // builder.Entity<User>().Property(b => b.LastActive).HasDefaultValueSql("getdate()");

            builder.Entity<Like>()
                .HasKey(k => new {k.LikerId, k.LikeeId});
            builder.Entity<Like>()
                .HasOne(u => u.Likee)
                .WithMany(u => u.Likers)
                .HasForeignKey(u => u.LikeeId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Like>()
                .HasOne(u => u.Liker)
                .WithMany(u => u.Likees)
                .HasForeignKey(u => u.LikerId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Value> Values { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<NewsItemEntity> NewsItemEntities { get; set; }
        public DbSet<NewsGroup> NewsGroups { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Like> Likes { get; set; }
    }
}