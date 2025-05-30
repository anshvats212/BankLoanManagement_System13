// C:\bankLoanmanagement133copy\BankLoan_Management133\BankLoan_Management133.Repositoryy\Models\DatabaseContext.cs
using BankLoan_Management133.Repositoryy.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankLoan_Management133.Models
{
    public class DatabaseContext : IdentityDbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> contextOptions) : base(contextOptions) { }

        public DbSet<customer> customers { get; set; }
        public DbSet<LoanProduct> LoanProducts { get; set; }

        public DbSet<Loan> Loans { get; set; }

        public DbSet<Repayment> Repayments { get; set; } 
public DbSet<LoanApplicationEntites> loanApplicationEntites { get; set; } 
    }
}