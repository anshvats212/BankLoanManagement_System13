using BankLoan_Management133.Models;
using BankLoan_Management133.Repositoryy;

namespace BankLoan_Management133.Repository
{
    public class RepaymentRepository : IRepaymentRepository
    {
        private readonly DatabaseContext _dbContext;

        public RepaymentRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Repayment> GetRepaymentSchedule(int applicationId)
        {
            return _dbContext.Repayments
                            .Where(r => r.ApplicationId == applicationId)
                            .OrderBy(r => r.DueDate)
                            .ToList();
        }

        public void AddRepayments(List<Repayment> repayments)
        {
            _dbContext.Repayments.AddRange(repayments);
            _dbContext.SaveChanges();
        }

        public async Task<Repayment?> GetRepaymentByIdAsync(int repaymentId)
        {
            return await _dbContext.Repayments.FindAsync(repaymentId);
        }

        public void UpdateRepayment(Repayment repayment)
        {
            _dbContext.Repayments.Update(repayment);
            _dbContext.SaveChanges();
        }

        public decimal GetOutstandingBalance(int applicationId)
        {
            return _dbContext.Repayments
                            .Where(r => r.ApplicationId == applicationId && r.PaymentStatus == PaymentStatus.PENDING)
                            .Sum(r => r.AmountDue);
        }
    }
}