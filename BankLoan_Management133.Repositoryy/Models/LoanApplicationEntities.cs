using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankLoan_Management133.Repositoryy.Models
{
    public class LoanApplicationEntites
    {
        [Key]
        public int ApplicationId { get; set; }

        [ForeignKey("customer")]   [Required] // Navigation property name, see below
        public int CustomerId { get; set; }

        [ForeignKey("LoanProduct")]   [Required] // Navigation property name, see below
        public int Id { get; set; }


        [Required]
        [Column(TypeName = "decimal(18, 2)")] // Specify precision and scale
        public decimal LoanAmount { get; set; }

        [Required]
        public DateTime? ApplicationDate { get; set; }

        [Required]
        [MaxLength(20)] // Add a max length for the string
        public string ApprovalStatus { get; set; } // Use string for status, more flexible.  Consider an Enum in a real app.

        // Add a default constructor.  This is VERY important for EF to work correctly.
        public LoanApplicationEntites()
        {
            this.ApplicationDate = DateTime.Now; // Initialize to current date/time
            this.ApprovalStatus = "PENDING";  // Set default status
        }
    }
}