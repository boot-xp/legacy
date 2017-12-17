using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Admin.Web.Models
{
    public class AdminContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderLineItem> OrderLineItems { get; set; }

        public AdminContext(DbContextOptions<AdminContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(p => p.FirstName).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<User>().Property(p => p.LastName).IsRequired().HasMaxLength(255);

            modelBuilder.Entity<Customer>().HasKey(c => c.Id);
            modelBuilder.Entity<Customer>().Property(p => p.FirstName).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Customer>().Property(p => p.LastName).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Customer>().OwnsOne(p => p.Address).Property(a => a.AddressLine1).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Customer>().OwnsOne(p => p.Address).Property(a => a.City).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Customer>().OwnsOne(p => p.Address).Property(a => a.Country).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Customer>().OwnsOne(p => p.Address).Property(a => a.State).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Customer>().OwnsOne(p => p.Address).Property(a => a.ZipCode).IsRequired();
            modelBuilder.Entity<Customer>().HasMany(c => c.Orders).WithOne(o => o.Customer).IsRequired();
            
            modelBuilder.Entity<Order>().HasKey(o => o.Id);
            modelBuilder.Entity<Order>().Property(p => p.OrderDate).IsRequired();
            modelBuilder.Entity<Order>().HasOne(p => p.Customer).WithMany(c => c.Orders).IsRequired();
            modelBuilder.Entity<Order>().HasMany(o => o.LineItems).WithOne(l => l.Order).IsRequired();

            modelBuilder.Entity<OrderLineItem>().HasKey(l => l.Id);
            modelBuilder.Entity<OrderLineItem>().HasOne(l => l.Order).WithMany(o => o.LineItems).IsRequired();
            modelBuilder.Entity<OrderLineItem>().HasOne(l => l.Product).WithMany(p => p.OrderLineItems).IsRequired();
            modelBuilder.Entity<OrderLineItem>().Property(l => l.Quantity).IsRequired();

            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Product>().HasMany(p => p.OrderLineItems).WithOne(o => o.Product).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Cost).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Price).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(255);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}