using BankLoan_Management133.Models;
using BankLoan_Management133.Repositoryy;

namespace BankLoan_Management133.BusinessLogicc
{
    public class RepaymentService : IRepaymentService
    {
        private readonly IRepaymentRepository _repaymentRepository;
        private readonly ILoanRepository _loanRepository;

        public RepaymentService(IRepaymentRepository repaymentRepository, ILoanRepository loanRepository)
        {
            _repaymentRepository = repaymentRepository;
            _loanRepository = loanRepository;
        }

        public async Task<List<Repayment>> GenerateRepaymentScheduleAsync(int applicationId)
        {
            var loan = await _loanRepository.GetLoanByIdAsync(applicationId);
            if (loan == null)
            {
                return new List<Repayment>();
            }

            List<Repayment> repayments = new List<Repayment>();
            decimal monthlyInstallment = Math.Round((loan.LoanAmount+(loan.LoanAmount * loan.InterestRate/ 100))/ loan.TermInMonths, 2);
            DateTime dueDate = loan.StartDate;

            for (int i = 0; i < loan.TermInMonths; i++)
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