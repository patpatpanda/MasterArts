using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MasterArtsLibrary.Models;
using MasterArtsLibrary.ViewModels;

namespace MasterArtsWeb
{
    public class MyDbContext : IdentityDbContext<IdentityUser>
    {
       
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }


        public virtual DbSet<Order> Orders { get; set; }
        public DbSet<Consignee> Consignees { get; set; }
        public DbSet<Consignor> Consignors { get; set; }
         public DbSet<Customer> Customers { get; set; }
        public DbSet<Goods> Goods { get; set; }
        public DbSet<ApiResponse> ApiResponses { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Sailing> Sailings { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Total> Totals { get; set; }
        public DbSet<ApiResponseInland> Inlands { get; set; }
        public DbSet<ShippingRequest> Requests { get; set; }
        public DbSet<Dimension> Dimensions { get; set; }
        public DbSet<CustomerRates> CustomerRates { get; set; }
        public DbSet<Airport> Airports { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Definiera primärnyckel för IdentityUserLogin<string>
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(p => new { p.LoginProvider, p.ProviderKey });
        }
    }
}
