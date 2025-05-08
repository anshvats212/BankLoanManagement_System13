using BankLoan_Management133.Models;
using BankLoan_Management133.Repositoryy.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BankLoan_Management133.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DatabaseContext _db;

        public CustomerRepository(DatabaseContext context)
        {
            _db = context;
        }

        public customer GetCustomerById(int id)
        {
            return _db.customers.FirstOrDefault(a => a.CustomerId == id);
        }

        public void InsertCustomer(customer obj)
        {
            _db.customers.Add(obj);
            _db.SaveChanges();
        }

        public void UpdateCustomer(customer obj)
        {
            _db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
        }

        public List<customer> GetAllCustomers()
        {
            return _db.customers.ToList();
        }
        public void DeleteCustomer(int id)
        {
            var data = _db.customers.Find(id);
            _db.customers.Remove(data);
            _db.SaveChanges();
        }


         public customer GetByEmail(string email)
        {
            return _db.customers.FirstOrDefault(d => d.Email == email);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
