using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan_Management133.Repositoryy.Models
{
    public class LoanProduct
    {
        [Key]
        public int Id { get; set; }
        public string pname { get; set; }
        public int interest { get; set; }
        public int minAmount { get; set; }
        public int maxAmount { get; set; }
        public int tenure { get; set; }
    }
}
