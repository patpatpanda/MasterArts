using MasterArtsLibrary.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MasterArtsWeb
{
    public class MyDbContext : IdentityDbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<Nio> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ShippingService> ShippingServices { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Receiver> Receivers { get; set; }
        public DbSet<Sender> Senders { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Definiera primärnyckel för IdentityUserLogin<string>
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(p => new { p.LoginProvider, p.ProviderKey });


            modelBuilder.Entity<OrderDetails>()
       .HasOne(od => od.Sender)
       .WithMany()
       .HasForeignKey(od => od.SenderId)
       .OnDelete(DeleteBehavior.Restrict); // Ändra till DeleteBehavior.Restrict

            modelBuilder.Entity<OrderDetails>()
                .HasOne(od => od.Receiver)
                .WithMany()
                .HasForeignKey(od => od.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict); 
        }

    }
}
