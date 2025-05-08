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
        bool Login(string email, string password);
        void UpdateCustomerProfile(customer profileup);
        customer GetCustomerByEmail(string email);
        void DeleteCustomer(int id);


    }
}
