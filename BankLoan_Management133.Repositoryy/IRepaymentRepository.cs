using System.Collections.Generic;
using System.Threading.Tasks;
using BankLoan_Management133.Models;

namespace BankLoan_Management133.Repositoryy
{
    public interface IRepaymentRepository
    {
        List<Repayment> GetRepaymentSchedule(int applicationId);
        void AddRepayments(List<Repayment> repayments);
        Task<Repayment?> GetRepaymentByIdAsync(int repaymentId);
        void UpdateRepayment(Repayment repayment);
        decimal GetOutstandingBalance(int applicationId);
    }
}