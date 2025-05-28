using BankLoan_Management133.Models;
using BankLoan_Management133.Repositoryy;
using Microsoft.EntityFrameworkCore;

namespace BankLoan_Management133.Repository
{
    public class LoanRepository : ILoanRepository
    {
        private readonly DatabaseContext _dbContext;

        public LoanRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Loan?> GetLoanByIdAsync(int applicationId) // Corrected return type and made async
        {
            return await _dbContext.Loans.FirstOrDefaultAsync(l => l.ApplicationId == applicationId);
        }

        public async Task<List<Loan>> GetAllLoansAsync()
        {
            return await _dbContext.Loans.ToListAsync();
        }

        public void AddLoan(Loan loan)
        {
            _dbContext.Add(loan);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Loan?> GetLoanDetailsAsync(int? id) // Corrected return type and made async
        {
            if (id == null)
            {
                return null;
            }
            return await _dbContext.Loans.FirstOrDefaultAsync(m => m.ApplicationId == id);
        }
    }
}