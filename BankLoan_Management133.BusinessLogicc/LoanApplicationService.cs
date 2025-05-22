using BankLoan_Management133.Models;
using BankLoan_Management133.Repositoryy;
using BankLoan_Management133.Repositoryy.Models;
using System.Collections.Generic;

namespace BankLoan_Management133.BusinessLogicc
{
    public class LoanApplicationService : ILoanApplicationService
    {
        private readonly ILoanApplicationRepository _loanApplicationRepository;

        public LoanApplicationService(ILoanApplicationRepository loanApplicationRepository)
        {
            _loanApplicationRepository = loanApplicationRepository;
        }

        public LoanApplication GetLoanById(int id)
        {
            return _loanApplicationRepository.GetLoanById(id);
        }

        public List<LoanApplication> GetAllLoan()
        {
            return _loanApplicationRepository.GetAllLoan();
        }

        public void SaveLoan(LoanApplication Loan)
        {
            if (Loan.applicationId > 0)
            {
                _loanApplicationRepository.UpdateLoan(Loan);
            }
            else
            {
                _loanApplicationRepository.AddLoan(Loan);
            }
        }

        public void DeleteLoan(int id) // Changed method name to match the interface
        {
            var LoanToDelete = _loanApplicationRepository.GetLoanById(id);
            if (LoanToDelete != null)
            {
                _loanApplicationRepository.DeleteLoan(LoanToDelete);
            }
            // Consider adding error handling or logging if the employee doesn't exist
        }
    }
}