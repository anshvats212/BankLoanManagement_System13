using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankLoan_Management133.BusinessLogicc.Interfaces;
using BankLoan_Management133.Repositoryy.Interfaces;
using BankLoan_Management133.Repositoryy.Models;

namespace BankLoan_Management133.BusinessLogicc.Implementation
{
    public class LoanProductService : ILoanProductService
    {
        private readonly ILoanProductRepository _loanProductRepository;

        public LoanProductService(ILoanProductRepository loanProductRepository)
        {
            _loanProductRepository = loanProductRepository;
        }

        public IEnumerable<LoanProduct> GetAllLoanProducts()
        {
            // This calls the repository method which returns the correct type
            return _loanProductRepository.GetAll();
        }

        public LoanProduct? GetLoanProductById(int id)
        {
            // This calls the repository method which returns the correct type
            return _loanProductRepository.GetById(id);
        }

        public void CreateLoanProduct(LoanProduct product)
        {
            // Pass the object of the correct type to the repository
            _loanProductRepository.Add(product);
            _loanProductRepository.Save(); // Save changes after add
        }

        public void UpdateLoanProduct(LoanProduct product)
        {
            // Pass the object of the correct type to the repository
            _loanProductRepository.Update(product);
            _loanProductRepository.Save(); // Save changes after update
        }

        public void DeleteLoanProduct(int id)
        {
            // Find the entity first using the repository
            var productToDelete = _loanProductRepository.GetById(id);
            if (productToDelete != null)
            {
                // Pass the object of the correct type to the repository
                _loanProductRepository.Remove(productToDelete);
                _loanProductRepository.Save(); // Save changes after remove
            }
        }
    }
}
