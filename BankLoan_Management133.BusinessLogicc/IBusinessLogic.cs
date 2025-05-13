// C:\bankLoanmanagement133copy\BankLoan_Management133\BankLoan_Management133.BusinessLogicc\IBusinessLogic.cs
using BankLoan_Management133.Models;
using BankLoan_Management133.Repositoryy.Models;
using System.Collections.Generic;

namespace BankLoan_Management133.BusinessLogic
{
    public interface IBusinessLogic
    {
        customer GetCustomerById(int id);
        void SaveCustomer(customer obj);
        List<customer> GetAllCustomers();
        // bool Login(string email, string password); // Removed
        void UpdateCustomerProfile(customer profileup);
        customer GetCustomerByEmail(string email);
        void DeleteCustomer(int id);
    }
}