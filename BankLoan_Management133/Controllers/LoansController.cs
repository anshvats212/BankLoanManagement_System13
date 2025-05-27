using Microsoft.AspNetCore.Mvc;
using BankLoan_Management133.Models;
using BankLoan_Management133.Repositoryy;
using BankLoan_Management133.BusinessLogicc;

namespace BankLoan_Management133.Controllers
{
    public class LoansController : Controller
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IRepaymentService _repaymentService;

        public LoansController(ILoanRepository loanRepository, IRepaymentService repaymentService)
        {
            _loanRepository = loanRepository;
            _repaymentService = repaymentService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _loanRepository.GetAllLoansAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            var loan = await _loanRepository.GetLoanDetailsAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            return View(loan);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicationId,LoanAmount,InterestRate,TermInMonths,StartDate")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                _loanRepository.AddLoan(loan);
                await _loanRepository.SaveChangesAsync();

                await _repaymentService.GenerateRepaymentScheduleAsync(loan.ApplicationId);

                return RedirectToAction(nameof(Index));
            }
            return View(loan);
        }

        private async Task<bool> LoanExists(int id) // Make it async
        {
            var loan = await _loanRepository.GetLoanByIdAsync(id);
            return loan != null;
        }
    }
}