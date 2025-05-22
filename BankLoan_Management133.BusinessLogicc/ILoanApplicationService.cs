using System.Collections.Generic;
using BankLoan_Management133.Models;
using BankLoan_Management133.Repositoryy.Models;

namespace BankLoan_Management133.BusinessLogicc
{
    public interface ILoanApplicationService
    {
        LoanApplication GetLoanById(int id);
        List<LoanApplication> GetAllLoan();
        void SaveLoan(LoanApplication loan);
        void DeleteLoan(int id);
    }
}