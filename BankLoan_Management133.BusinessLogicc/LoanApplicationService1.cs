using BankLoan_Management133.Models;
using BankLoan_Management133.Repositoryy;
using BankLoan_Management133.Repositoryy.Models;
using System.Collections.Generic;

namespace BankLoan_Management133.BusinessLogicc
{
    public class LoanApplicationService1 : ILoanApplicationService1
    {
        private readonly ILoanApplicationRepository1 _loanApplicationRepository;

        public LoanApplicationService1(ILoanApplicationRepository1 loanApplicationRepository)
        {
            _loanApplicationRepository = loanApplicationRepository ?? throw new ArgumentNullException(nameof(loanApplicationRepository));
        }

        public LoanApplicationEntites ApplyForLoan(int customerId, int loanProductId, decimal loanAmount)
        {
            if (!ValidateLoanApplication(customerId, loanProductId, loanAmount))
            {
                throw new ArgumentException("Invalid loan application details.");
            }

            var LoanApplicationEntites = new LoanApplicationEntites
            {
                CustomerId = customerId,
                Id = loanProductId,
                LoanAmount = loanAmount,
                ApplicationDate = DateTime.Now, // Set ApplicationDate here
                ApprovalStatus = "PENDING" //and default status here
            };

            _loanApplicationRepository.Add(LoanApplicationEntites);
            return LoanApplicationEntites;
        }

        public LoanApplicationEntites GetApplicationStatus(int applicationId)
        {
            return _loanApplicationRepository.GetApplicationStatus(applicationId);
        }

        public void ProcessLoanApplication(int applicationId, string approvalStatus)
        {
            var application = _loanApplicationRepository.GetById(applicationId);
            if (application == null)
            {
                throw new ArgumentException("Application not found.");
            }

            //  Validate the status transition.  Important business rule.
            if (application.ApprovalStatus != "PENDING")
            {
                throw new InvalidOperationException($"Application status cannot be changed from {application.ApprovalStatus}.");
            }
            if (approvalStatus != "APPROVED" && approvalStatus != "REJECTED")
            {
                throw new ArgumentException("Invalid approval status.  Must be 'APPROVED' or 'REJECTED'.");
            }

            application.ApprovalStatus = approvalStatus;
            _loanApplicationRepository.Update(application);
        }

        public bool ValidateLoanApplication(int customerId, int loanProductId, decimal loanAmount)
        {
            // Basic validation - expand as needed.
            return customerId > 0 && loanProductId > 0 && loanAmount > 0;
        }

        // Implement the GetById method:
        public LoanApplicationEntites GetById(int id)
        {
            return _loanApplicationRepository.GetById(id);
        }

        public IEnumerable<LoanApplicationEntites> GetAllApplications()
        {
            return _loanApplicationRepository.GetAll();
        }
    }
}