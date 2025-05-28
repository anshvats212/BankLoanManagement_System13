using Microsoft.AspNetCore.Mvc;
using BankLoan_Management133.BusinessLogicc;

namespace BankLoan_Management133.Controllers
{
    public class RepaymentController : Controller
    {
        private readonly IRepaymentService _repaymentService;

        public RepaymentController(IRepaymentService repaymentService)
        {
            _repaymentService = repaymentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRepaymentSchedule(int id)
        {
            var repaymentSchedule = await _repaymentService.GetRepaymentScheduleAsync(id);

            if (!repaymentSchedule.Any())
            {
                TempData["InfoMessage"] = "No repayment schedule found for this loan.";
            }

            return View(repaymentSchedule);
        }


        [HttpGet]
        public IActionResult GetOutstandingBalance(int applicationId, string returnUrl = null)
        {
            decimal outstandingBalance = _repaymentService.GetOutstandingBalance(applicationId);
            ViewBag.OutstandingBalance = outstandingBalance;
            ViewBag.ReturnUrl = returnUrl; // Store the return URL in ViewBag
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ProcessPayment(int repaymentId)
        {
            var repayment = await _repaymentService.GetRepaymentByIdAsync(repaymentId);

            if (repayment == null)
            {
                TempData["ErrorMessage"] = "Invalid repayment ID.";
                return RedirectToAction(nameof(GetRepaymentSchedule), new { id = repayment?.ApplicationId });
            }

            return View(repayment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessPayment(int repaymentId, [Bind("RepaymentId")] BankLoan_Management133.Models.Repayment repayment)
        {
            if (repaymentId != repayment.RepaymentId)
            {
                return NotFound();
            }

            var repaymentToUpdate = await _repaymentService.GetRepaymentByIdAsync(repaymentId);

            if (repaymentToUpdate == null)
            {
                return NotFound();
            }

            if (repaymentToUpdate.PaymentStatus == BankLoan_Management133.Models.PaymentStatus.COMPLETED)
            {
                TempData["ErrorMessage"] = "This payment has already been processed.";
                return RedirectToAction(nameof(GetRepaymentSchedule), new { id = repaymentToUpdate.ApplicationId });
            }

            repaymentToUpdate.PaymentDate = DateTime.Now;
            repaymentToUpdate.PaymentStatus = BankLoan_Management133.Models.PaymentStatus.COMPLETED;

            await _repaymentService.UpdateRepaymentAsync(repaymentToUpdate);
            TempData["SuccessMessage"] = "Payment processed successfully!";
            return RedirectToAction(nameof(GetRepaymentSchedule), new { id = repaymentToUpdate.ApplicationId });
        }
    }
}