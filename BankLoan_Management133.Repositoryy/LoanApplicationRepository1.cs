using BankLoan_Management133.Models;
using BankLoan_Management133.Repositoryy.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BankLoan_Management133.Repositoryy.Models
{
    public class LoanApplicationRepository1 : ILoanApplicationRepository1
    {
        private readonly DatabaseContext _context;

        public LoanApplicationRepository1(DatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public LoanApplicationEntites GetById(int id)
        {
            return _context.loanApplicationEntites.Find(id);
        }

        public void Add(LoanApplicationEntites application)
        {
            _context.loanApplicationEntites.Add(application);
            _context.SaveChanges();
        }

        public void Update(LoanApplicationEntites application)
        {
            _context.Entry(application).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public LoanApplicationEntites GetApplicationStatus(int applicationId)
        {
            return _context.loanApplicationEntites.Find(applicationId);
        }
         // Implement the GetAll method:
        public IEnumerable<LoanApplicationEntites> GetAll()
        {
            return _context.loanApplicationEntites.ToList();
        }
    }
}