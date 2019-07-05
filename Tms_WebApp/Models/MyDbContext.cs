using System.Data.Entity;

namespace Tms_WebApp.Models
{
    public class MyDbContext:DbContext
    {
        public DbSet<Post> Posts{ get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Venue> Venues { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet <Attendance> Attendances { get; set; }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasRequired(p => p.User)
                .WithMany()
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Picture>()
                .HasRequired(u => u.User)
                .WithMany()
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Attendance>()
                .HasRequired(a => a.Post)
                .WithMany(p=>p.Attendances)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserNotification>()
                .HasRequired(u => u.User)
                .WithMany(u=>u.UserNotifications)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}