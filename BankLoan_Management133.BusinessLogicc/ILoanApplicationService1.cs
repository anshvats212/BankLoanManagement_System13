using BankLoan_Management133.Repositoryy.Models;

namespace BankLoan_Management133.BusinessLogicc
{
    public interface ILoanApplicationService1
    {
        LoanApplicationEntites ApplyForLoan(int customerId, int loanProductId, decimal loanAmount);
        LoanApplicationEntites GetApplicationStatus(int applicationId);
        void ProcessLoanApplication(int applicationId, string approvalStatus);
        bool ValidateLoanApplication(int customerId, int loanProductId, decimal loanAmount);

        // Add this line:
        LoanApplicationEntites GetById(int id);
        IEnumerable<LoanApplicationEntites> GetAllApplications();

    }
}