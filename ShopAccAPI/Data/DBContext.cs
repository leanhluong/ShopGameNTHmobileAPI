using Microsoft.EntityFrameworkCore;
using ShopAccAPI.Models;

namespace ShopAccAPI.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Game> Games { get; set; } = null!;
        public DbSet<Account> Accounts { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Transaction> Transactions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.Order)
                .WithOne(o => o.Account)
                .HasForeignKey<Order>(o => o.AccountId);
        }
    }
}
