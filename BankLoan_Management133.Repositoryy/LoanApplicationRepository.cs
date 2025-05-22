using BankLoan_Management133.Models;
using BankLoan_Management133.Repositoryy.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BankLoan_Management133.Repositoryy
{
    public class LoanApplicationRepository:ILoanApplicationRepository
    {
        private readonly DatabaseContext _dbContext;

        public LoanApplicationRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public LoanApplication GetLoanById(int id)
        {
            return _dbContext.LoanApplications.Find(id);
        }

        public List<LoanApplication> GetAllLoan()
        {
            return _dbContext.LoanApplications.ToList();
        }

        public void AddLoan(LoanApplication Loan)
        {
            _dbContext.LoanApplications.Add(Loan);
            _dbContext.SaveChanges();
        }

        public void UpdateLoan(LoanApplication Loan)
        {
            _dbContext.Entry(Loan).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void DeleteLoan(LoanApplication Loan)
        {
            _dbContext.LoanApplications.Remove(Loan);
            _dbContext.SaveChanges();
        }
    }
}