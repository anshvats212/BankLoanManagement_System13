using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BankLoan_Management133.Models
{
    
    public class Repayment
    {
        [Key]
        public int RepaymentId { get; set; }

        [ForeignKey("Loan")] // Assuming you have a LoanApplication model
        public int ApplicationId { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal AmountDue { get; set; }

        public DateTime? PaymentDate { get; set; }

        [Required]
        public PaymentStatus PaymentStatus { get; set; }

        // Navigation property (if you have a Loan model)
        public Loan? Loan { get; set; }
    }

    public enum PaymentStatus
    {
        PENDING,
        COMPLETED
    }
}
