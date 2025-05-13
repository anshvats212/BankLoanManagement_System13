// C:\bankLoanmanagement133copy\BankLoan_Management133\BankLoan_Management133.Repositoryy\Models\customer.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BankLoan_Management133.Repositoryy.Models
{
    public class customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        // public string Passward { get; set; } // Removed
        public string Phone { get; set; }
        public string Address { get; set; }
        public string KycStatus { get; set; }
    }
}