using Microsoft.EntityFrameworkCore;

namespace Admin.Web.Models
{
    public class UsersContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().Property(p => p.Email).IsRequired().HasMaxLength(1024);
            modelBuilder.Entity<User>().Property(p => p.FirstName).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<User>().Property(p => p.LastName).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<User>().Property(p => p.Password).IsRequired().HasMaxLength(255);
            base.OnModelCreating(modelBuilder);
        }
    }
}