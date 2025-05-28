using BankLoan_Management133.Models;

namespace BankLoan_Management133.BusinessLogicc
{
    public interface IRepaymentService
    {
        Task<List<Repayment>> GenerateRepaymentScheduleAsync(int applicationId);
        decimal GetOutstandingBalance(int applicationId);
        Task<List<Repayment>> GetRepaymentScheduleAsync(int applicationId);
        Task<Repayment?> GetRepaymentByIdAsync(int repaymentId);
        Task UpdateRepaymentAsync(Repayment repayment);
    }
}