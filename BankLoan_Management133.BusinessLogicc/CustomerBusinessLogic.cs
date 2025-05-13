// C:\bankLoanmanagement133copy\BankLoan_Management133\BankLoan_Management133.BusinessLogicc\CustomerBusinessLogic.cs
using BankLoan_Management133.Models;
using BankLoan_Management133.Repository;
using BankLoan_Management133.Repositoryy.Models;
using System.Collections.Generic;

namespace BankLoan_Management133.BusinessLogic
{
    public class CustomerBusinessLogic : IBusinessLogic
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerBusinessLogic(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public customer GetCustomerById(int id)
        {
            return _customerRepository.GetCustomerById(id);
        }

        public void SaveCustomer(customer obj)
        {
            if (obj.CustomerId > 0)
            {
                _customerRepository.UpdateCustomer(obj);
            }
            else
            {
                _customerRepository.InsertCustomer(obj);
            }
        }

        public customer GetCustomerByEmail(string email)
        {
            return _customerRepository.GetByEmail(email);
        }
        // public bool Login(string email,string password) // Removed
        // {
        //     var data = _customerRepository.GetByEmail(email);
        //     return data != null && data.Passward == password;
        // }

        public void UpdateCustomerProfile(customer profile)
        {
            _customerRepository.UpdateCustomer(profile);
            _customerRepository.Save();
        }
        public List<customer> GetAllCustomers()
        {
            return _customerRepository.GetAllCustomers();
        }
        public void DeleteCustomer(int id)
        {
            _customerRepository.DeleteCustomer(id);
        }
    }
}