using Microsoft.EntityFrameworkCore;
using OnlineBankingApp.Entity.Model;

namespace OnlineBankingApp.Entity.DbContexts
{
    public class BankingContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public BankingContext(DbContextOptions<BankingContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(a => a.Version)
                .IsConcurrencyToken(); // Concurrency kontrol token'ı olarak ayarla
        }
    }
}
