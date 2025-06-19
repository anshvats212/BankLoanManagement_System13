using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankLoan_Management133.Models
{
    public class Repayment
    {
        [Key]
        public int RepaymentId { get; set; }

        [ForeignKey("LoanApplication")] // Links to the loan application
        public int ApplicationId { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal AmountDue { get; set; }

        public DateTime? PaymentDate { get; set; }

        [Required]
        public PaymentStatus PaymentStatus { get; set; }
    }

    public enum PaymentStatus
    {
        PENDING,
        COMPLETED
    }
}