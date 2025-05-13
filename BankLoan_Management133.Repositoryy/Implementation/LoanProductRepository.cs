using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankLoan_Management133.Models;
using BankLoan_Management133.Repositoryy.Data;
using BankLoan_Management133.Repositoryy.Interfaces;
using BankLoan_Management133.Repositoryy.Models;


namespace BankLoan_Management133.Repositoryy.Implementation
{

    public class LoanProductRepository : ILoanProductRepository
    {
        private readonly DatabaseContext _db;

        public LoanProductRepository(DatabaseContext db)
        {
            _db = db;
        }

        public IEnumerable<LoanProduct> GetAll()
        {
            // The return type of _db.LoanProducts.ToList() is List<LoanProduct>
            // where LoanProduct is resolved using the using in the DbContext file.
            // This matches the required IEnumerable<LoanProduct.Repository.Models.LoanProduct>
            return _db.LoanProducts.ToList();
        }

        public LoanProduct? GetById(int id)
        {
            // _db.LoanProducts.Find expects/returns a type that matches the DbSet type
            return _db.LoanProducts.Find(id);
        }

        public void Add(LoanProduct product)
        {
            // The parameter type matches the DbSet type
            _db.LoanProducts.Add(product);
        }

        public void Update(LoanProduct product)
        {
            // The parameter type matches the DbSet type
            _db.LoanProducts.Update(product);
        }

        public void Remove(LoanProduct product)
        {
            // The parameter type matches the DbSet type
            _db.LoanProducts.Remove(product);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
