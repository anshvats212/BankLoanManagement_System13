using BankLoan_Management133.Repositoryy.Models;
using Microsoft.EntityFrameworkCore;

namespace BankLoan_Management133.Models
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> contextOptions) : base(contextOptions) { }
 
        public DbSet<customer> customers { get; set; }
    }
}
