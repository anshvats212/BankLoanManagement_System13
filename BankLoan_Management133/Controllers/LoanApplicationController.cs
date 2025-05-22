using Microsoft.AspNetCore.Mvc;
using BankLoan_Management133.Models;
using BankLoan_Management133.BusinessLogicc;
using BankLoan_Management133.Repositoryy.Models;
using Microsoft.AspNetCore.Identity;
using System.Numerics;
using System.Collections.Generic;

namespace BankLoan_Management133.Controllers
{
    public class LoanApplicationController : Controller
    {
        private readonly ILoanApplicationService _loanApplicationService;

        public LoanApplicationController(ILoanApplicationService loanApplicationService)
        {
            _loanApplicationService = loanApplicationService;
        }

        public IActionResult Index(int id = 0)
        {
            LoanApplication obj = new LoanApplication();
            ViewBag.st = "Submit";
            if (id > 0)
            {
                obj = _loanApplicationService.GetLoanById(id);
                if (obj != null)
                {
                    ViewBag.st = "Update";
                }
                else
                {
                    // Handle the case where the employee with the given ID is not found
                    return NotFound(); // Or redirect to an error page
                }
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Index(LoanApplication obj) // Changed to Index
        {
            _loanApplicationService.SaveLoan(obj);
            return RedirectToAction("ShowLoan");
        }

        public IActionResult ShowLoan()
        {
            var data = _loanApplicationService.GetAllLoan();
            return View(data);
        }

        public IActionResult Delete(int id = 0)
        {
            _loanApplicationService.DeleteLoan(id);
            return RedirectToAction("ShowLoan");
        }
    }
}