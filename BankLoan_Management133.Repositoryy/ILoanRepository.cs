using BankLoan_Management133.Models;
using System.Collections.Generic;

namespace BankLoan_Management133.Repositoryy
{
    public interface ILoanRepository
    {
        Task<Loan?> GetLoanByIdAsync(int applicationId); // Corrected return type and made async
        Task<List<Loan>> GetAllLoansAsync();
        void AddLoan(Loan loan);
        Task SaveChangesAsync();
        Task<Loan?> GetLoanDetailsAsync(int? id); // Corrected return type and made async
    }
}