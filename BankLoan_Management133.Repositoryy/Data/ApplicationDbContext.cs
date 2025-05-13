using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankLoan_Management133.Repositoryy.Models;
using Microsoft.EntityFrameworkCore;

namespace BankLoan_Management133.Repositoryy.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<LoanProduct> LoanProducts { get; set; }
    }
}
