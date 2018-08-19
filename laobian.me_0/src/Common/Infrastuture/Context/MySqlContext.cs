using Laobian.Infrasture.Entity.User;
using Laobian.Infrastuture.Entity.Blog;
using Laobian.Infrastuture.Entity.Log;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Laobian.Infrasture.Context
{
    public class MySqlContext : DbContext
    {
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }

        public DbSet<BlogPost> BlogPosts { get; set; }

        public DbSet<Log4Common> Log4Common { get; set; }

        public DbSet<Log4Admin> Log4Admin { get; set; }

        public DbSet<Log4BlogVisit> Log4BlogVisit { get; set; }

        public DbSet<Log4User> Log4User { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<UserPermission> UserPermission { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogPost>()
                .HasIndex(_ => _.Publish)
                .IsUnique(false);
            modelBuilder.Entity<BlogPost>()
                .HasIndex(_ => _.CreateTime)
                .IsUnique(false);
            modelBuilder.Entity<BlogPost>()
                .HasIndex(_ => _.Url)
                .IsUnique(true);
            modelBuilder.Entity<BlogPost>()
                .HasIndex(_ => _.Tags)
                .IsUnique(false);

            modelBuilder.Entity<UserPermission>()
                .HasOne(p => p.User)
                .WithMany(u => u.UserPermission)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasIndex(_ => _.UserName)
                .IsUnique(true);
            modelBuilder.Entity<User>()
                .HasIndex(_ => _.Email)
                .IsUnique(true);

            modelBuilder.Entity<Log4BlogVisit>()
                .HasIndex(_ => _.Component)
                .IsUnique(false);
            modelBuilder.Entity<Log4BlogVisit>()
                .HasIndex(_ => _.PageId)
                .IsUnique(false);
        }
    }
}
