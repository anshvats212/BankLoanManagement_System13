using BankLoan_Management133.Repositoryy.Models;

namespace BankLoan_Management133.BusinessLogicc
{
    public class LoanApplicationService1 : ILoanApplicationService1
    {
        private readonly ILoanApplicationRepository1 _loanApplicationRepository;
        private readonly IRepaymentService _repaymentService; // Inject Repayment Service
        // Removed ILoanRepository

        public LoanApplicationService1(
            ILoanApplicationRepository1 loanApplicationRepository,
            IRepaymentService repaymentService) // Removed ILoanRepository from constructor
        {
            _loanApplicationRepository = loanApplicationRepository ?? throw new ArgumentNullException(nameof(loanApplicationRepository));
            _repaymentService = repaymentService ?? throw new ArgumentNullException(nameof(repaymentService));
        }

        public LoanApplicationEntites ApplyForLoan(int customerId, int loanProductId, decimal loanAmount)
        {
            if (!ValidateLoanApplication(customerId, loanProductId, loanAmount))
            {
                throw new ArgumentException("Invalid loan application details.");
            }

            var loanApplication = new LoanApplicationEntites
            {
                CustomerId = customerId,
                Id = loanProductId, // Assuming this is the LoanProductId
                LoanAmount = loanAmount,
                ApplicationDate = DateTime.Now,
                ApprovalStatus = "PENDING"
                // InterestRate and TermInMonths might be set here if defaults, or later during processing
            };

            _loanApplicationRepository.Add(loanApplication);
            return loanApplication;
        }

        public LoanApplicationEntites GetApplicationStatus(int applicationId)
        {
            return _loanApplicationRepository.GetApplicationStatus(applicationId);
        }

        public async void ProcessLoanApplication(int applicationId, string approvalStatus) // Made async
        {
            var application = _loanApplicationRepository.GetById(applicationId);
            if (application == null)
            {
                throw new ArgumentException("Application not found.");
            }

            if (application.ApprovalStatus != "PENDING")
            {
                throw new InvalidOperationException($"Application status cannot be changed from {application.ApprovalStatus}.");
            }
            if (approvalStatus != "APPROVED" && approvalStatus != "REJECTED")
            {
                throw new ArgumentException("Invalid approval status. Must be 'APPROVED' or 'REJECTED'.");
            }

            application.ApprovalStatus = approvalStatus;

            // --- IMPORTANT: Set InterestRate and TermInMonths when approving ---
            if (approvalStatus == "APPROVED")
            {
                application.InterestRate = 12.0m;
                application.TermInMonths = 24;
                if (application.LoanAmount <= 500000 && application.LoanAmount > 0)
                {
                    application.InterestRate = 8.0m;
                    application.TermInMonths = 18;
                }
                else if (application.LoanAmount > 500000 && application.LoanAmount <= 1500000)
                {
                    application.InterestRate = 12.0m;
                    application.TermInMonths = 24;
                }
                else if (application.LoanAmount > 1500000)
                {
                    application.InterestRate = 12.0m;
                    application.TermInMonths = 36;
                }
                else
                {
                    application.InterestRate = 12.0m;
                    application.TermInMonths = 36;
                }

            }
            // --- END IMPORTANT ---

            _loanApplicationRepository.Update(application);

            // If approved, generate repayment schedule
            if (approvalStatus == "APPROVED")
            {
                // Now generate the repayment schedule using the updated LoanApplicationEntites
                await _repaymentService.GenerateRepaymentScheduleAsync(application.ApplicationId);
            }
        }

        public bool ValidateLoanApplication(int customerId, int loanProductId, decimal loanAmount)
        {
            return customerId > 0 && loanProductId > 0 && loanAmount > 0;
        }

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