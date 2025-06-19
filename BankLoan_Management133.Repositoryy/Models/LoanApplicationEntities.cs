using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankLoan_Management133.Repositoryy.Models
{
    public class LoanApplicationEntites
    {
        [Key]
        public int ApplicationId { get; set; }

        [ForeignKey("customer")]
        [Required]
        public int CustomerId { get; set; }

        [ForeignKey("LoanProduct")]
        [Required]
        public int Id { get; set; } // This is likely LoanProductId

        [Required]
        [Column(TypeName = "decimal(18, 2)")] // Specify precision and scale
        public decimal LoanAmount { get; set; }

        [Required]
        public DateTime? ApplicationDate { get; set; }

        [Required]
        [MaxLength(20)] // Add a max length for the string
        public string ApprovalStatus { get; set; } // Use string for status, more flexible. Consider an Enum in a real app.



        //----------------------------------------------------------------------------------------------------------------------------

        // --- NEW PROPERTIES FOR REPAYMENT CALCULATION ---
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal InterestRate { get; set; } // Add this

        [Required]
        public int TermInMonths { get; set; } // Add this
        public LoanApplicationEntites()
        {
            this.ApplicationDate = DateTime.Now; // Initialize to current date/time
            this.ApprovalStatus = "PENDING"; // Set default status
            this.InterestRate = 0.0m; // Default or fetched from LoanProduct
            this.TermInMonths = 0; // Default or fetched from LoanProduct
        }
    }
}