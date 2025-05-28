using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankLoan_Management133.Models
{
    public class Loan
    {

        [Required]
        [Key]
        public int ApplicationId { get; set; } //  Key for Repayment

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal LoanAmount { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal InterestRate { get; set; } // Interest rate in percentage

        [Required]
        public int TermInMonths { get; set; } // Loan term in months

        [Required]
        public DateTime StartDate { get; set; } // Loan start date

        // Navigation property for Repayments (if configured)
        public ICollection<Repayment>? Repayments { get; set; }
    }
}
