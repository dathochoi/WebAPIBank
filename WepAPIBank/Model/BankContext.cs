using Microsoft.EntityFrameworkCore;

namespace WepAPIBank.Model
{
    public class BankContext: DbContext
    {
        public BankContext(DbContextOptions<BankContext> options):base (options)
        {

        }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
    }
}
