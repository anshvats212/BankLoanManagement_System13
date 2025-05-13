using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankLoan_Management133.Repositoryy.Models;

namespace BankLoan_Management133.Repositoryy.Interfaces
{
    public interface ILoanProductRepository
    {
        IEnumerable<LoanProduct> GetAll();
        LoanProduct? GetById(int id);
        void Add(LoanProduct product);
        void Update(LoanProduct product);
        void Remove(LoanProduct product);
        void Save();
    }
}
