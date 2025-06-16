using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankLoan_Management133.Models;
using BankLoan_Management133.Repositoryy; // For IRepaymentRepository
using BankLoan_Management133.Repositoryy.Models; // For LoanApplicationEntites, ILoanApplicationRepository1

namespace BankLoan_Management133.BusinessLogicc
{
    public class RepaymentService : IRepaymentService
    {
        private readonly IRepaymentRepository _repaymentRepository;
        // Changed from ILoanApplicationService1 to ILoanApplicationRepository1
        private readonly ILoanApplicationRepository1 _loanApplicationRepository;

        public RepaymentService(IRepaymentRepository repaymentRepository, ILoanApplicationRepository1 loanApplicationRepository) // Corrected constructor
        {
            _repaymentRepository = repaymentRepository;
            _loanApplicationRepository = loanApplicationRepository; // Initialize the new dependency
        }

        public async Task<List<Repayment>> GenerateRepaymentScheduleAsync(int applicationId)
        {
            // Fetch LoanApplicationEntites directly using the repository
            var loanApplication = _loanApplicationRepository.GetById(applicationId); // Use GetById from the repository
            if (loanApplication == null)
            {
                return new List<Repayment>();
            }

            List<Repayment> repayments = new List<Repayment>();
            decimal monthlyInstallment = Math.Round((loanApplication.LoanAmount + (loanApplication.LoanAmount * loanApplication.InterestRate / 100)) / loanApplication.TermInMonths, 2);
            DateTime dueDate = loanApplication.ApplicationDate ?? DateTime.Today;

            for (int i = 0; i < loanApplication.TermInMonths; i++)
            {
                var repayment = new Repayment
                {
                    ApplicationId = applicationId,
                    DueDate = dueDate,
                    AmountDue = monthlyInstallment,
                    PaymentStatus = PaymentStatus.PENDING
                };

                repayments.Add(repayment);
                dueDate = dueDate.AddMonths(1);
            }

            _repaymentRepository.AddRepayments(repayments);
            return repayments;
        }

        public decimal GetOutstandingBalance(int applicationId)
        {
            return _repaymentRepository.GetOutstandingBalance(applicationId);
        }

        public async Task<List<Repayment>> GetRepaymentScheduleAsync(int applicationId)
        {
            return _repaymentRepository.GetRepaymentSchedule(applicationId);
        }

        public async Task<Repayment?> GetRepaymentByIdAsync(int repaymentId)
        {
            return await _repaymentRepository.GetRepaymentByIdAsync(repaymentId);
        }

        public async Task UpdateRepaymentAsync(Repayment repayment)
        {
            _repaymentRepository.UpdateRepayment(repayment);
            await Task.CompletedTask;
        }
    }
}