using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankLoan_Management133.Repositoryy.Models;

namespace BankLoan_Management133.Repositoryy.Models // Adjust to your actual namespace
{
    public class LoanApplication
    {
        [Key]
        public int applicationId { get; set; }

        [ForeignKey("customer")] // Navigation property name, see below
        public int CustomerId { get; set; }

        [ForeignKey("LoanProduct")] // Navigation property name, see below
        public int Id { get; set; }

        public decimal loanAmount { get; set; }

        public DateTime applicationDate { get; set; }

        public string approvalStatus { get; set; } // Could also use an enum (see below)

        // Navigation Properties (Optional, but highly recommended for EF Core)
        public virtual customer? customer { get; set; } //  ? makes it nullable
        public virtual LoanProduct? LoanProduct { get; set; } // ? makes it nullable
    }

    // You could define an enum for approval status:
    public enum ApprovalStatus
    {
        PENDING,
        APPROVED,
        REJECTED
    }
}
