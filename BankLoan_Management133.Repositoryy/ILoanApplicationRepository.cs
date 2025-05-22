using System.Collections.Generic;    // Assuming your model is in the top-level project
using BankLoan_Management133.Repositoryy.Models;

namespace BankLoan_Management133.Repositoryy
{
    public interface ILoanApplicationRepository
    {
        LoanApplication GetLoanById(int id);
        List<LoanApplication> GetAllLoan();
        void AddLoan(LoanApplication Loan);
        void UpdateLoan(LoanApplication Loan);
        void DeleteLoan(LoanApplication Loan);
    }
}