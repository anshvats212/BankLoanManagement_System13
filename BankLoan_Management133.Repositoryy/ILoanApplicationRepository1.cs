using System.Collections.Generic;    // Assuming your model is in the top-level project
using BankLoan_Management133.Repositoryy.Models;

namespace BankLoan_Management133.Repositoryy.Models
{
    public interface ILoanApplicationRepository1
    {
        LoanApplicationEntites GetById(int id);
        void Add(LoanApplicationEntites application);
        void Update(LoanApplicationEntites application);
        //void Delete(int id); //Consider adding delete if needed
        LoanApplicationEntites GetApplicationStatus(int applicationId);
        IEnumerable<LoanApplicationEntites> GetAll();

    }
}