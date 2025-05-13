using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankLoan_Management133.Repositoryy.Models;

namespace BankLoan_Management133.BusinessLogicc.Interfaces
{

    public interface ILoanProductService
    {
        IEnumerable<LoanProduct> GetAllLoanProducts();
        LoanProduct? GetLoanProductById(int id);
        void CreateLoanProduct(LoanProduct product);
        void UpdateLoanProduct(LoanProduct product);
        void DeleteLoanProduct(int id);
        // Add methods for any business-specific operations
    }
}
