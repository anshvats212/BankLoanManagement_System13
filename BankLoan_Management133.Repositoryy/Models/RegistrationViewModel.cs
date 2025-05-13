// C:\bankLoanmanagement133copy\BankLoan_Management133\BankLoan_Management133.Repositoryy\Models\RegisterViewModel.cs
using System.ComponentModel.DataAnnotations;

namespace BankLoan_Management133.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }
        public string Kyc { get; set; }
    }
}