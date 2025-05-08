using BankLoan_Management133.Models;
using BankLoan_Management133.Repositoryy.Models;
using System.Collections.Generic;

namespace BankLoan_Management133.Repository
{
    public interface ICustomerRepository
    {
        customer GetCustomerById(int id);
        void InsertCustomer(customer obj);
        void UpdateCustomer(customer obj);
        customer GetByEmail(string email);
        List<customer> GetAllCustomers();
        void DeleteCustomer(int id);
        void Save();
    }
}
