using Microsoft.EntityFrameworkCore;
using OnlineBankingApp.Entity.Model;

namespace OnlineBankingApp.Entity.DbContexts
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }
    }
}
