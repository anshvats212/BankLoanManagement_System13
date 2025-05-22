using BankLoan_Management133.BusinessLogicc;
using BankLoan_Management133.Repositoryy.Models; // Make sure this namespace is included
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; // For SelectList
using System;
using System.Threading.Tasks; // For asynchronous operations (recommended for data access)

namespace BankLoan_Management133.Controllers
{
    public class LoanApplication1Controller : Controller // Inherit from Controller for views
    {
        private readonly ILoanApplicationService1 _loanApplicationService;

        public LoanApplication1Controller(ILoanApplicationService1 loanApplicationService)
        {
            _loanApplicationService = loanApplicationService ?? throw new ArgumentNullException(nameof(loanApplicationService));
        }

        [HttpGet("Apply")]
        public IActionResult Apply()
        {
            // Consider passing data needed for dropdowns (e.g., list of loan products)
            return View();
        }

        [HttpPost("Apply")]
        [ValidateAntiForgeryToken] // Important for security
        public IActionResult Apply(int CustomerId, int Id, decimal LoanAmount)
        {
            if (ModelState.IsValid) // Check if the submitted data is valid based on model attributes
            {
                try
                {
                    var application = _loanApplicationService.ApplyForLoan(CustomerId, Id, LoanAmount);
                    ViewBag.ApplicationId = application.ApplicationId; // Pass ApplicationId to the view
                    ViewBag.SuccessMessage = "Your loan application has been submitted successfully. Your Application ID is: " + application.ApplicationId;
                    return View(); // Return the Apply view to show the success message
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message); // Add error to ModelState
                }
                catch (Exception ex)
                {
                    // Log the error!
                    ModelState.AddModelError(string.Empty, "An unexpected error occurred while submitting your application.");
                }
            }
            // If ModelState is not valid or an exception occurred, return the view with errors
            return View();
        }
        [HttpGet("GetApplicationStatus")]
        public IActionResult GetApplicationStatus(int applicationId)
        {
            if (applicationId <= 0)
            {
                ViewBag.StatusErrorMessage = "Please enter a valid Application ID.";
                return View();
            }

            try
            {
                var application = _loanApplicationService.GetApplicationStatus(applicationId);
                if (application == null)
                {
                    ViewBag.StatusErrorMessage = "Application not found.";
                    return View(); // Return the Status view with the error
                }
                ViewBag.Status = application.ApprovalStatus; // Pass status to the view
                return View(); // Return the Status view with the status
            }
            catch (Exception ex)
            {
                // Log the error!
                ViewBag.StatusErrorMessage = "An unexpected error occurred while fetching the status.";
                return View(); // Return the Status view with the error
            }
        }

        [HttpGet("Process/{applicationId}")]
        public IActionResult Process(int applicationId)
        {
            Console.WriteLine($"Process action called with applicationId: {applicationId}"); // Add this line
            try
            {
                var application = _loanApplicationService.GetById(applicationId);
                if (application == null)
                {
                    return NotFound();
                }
                return View(application);
            }
            catch (Exception ex)
            {
                ViewBag.ProcessErrorMessage = "Error loading application for processing.";
                return View();
            }
        }

        [HttpPost("Process/{applicationId}")]
        [ValidateAntiForgeryToken]
        public IActionResult ProcessLoanApplication(int applicationId, string approvalStatus)
        {
            if (!string.IsNullOrEmpty(approvalStatus) && (approvalStatus.ToUpper() == "APPROVED" || approvalStatus.ToUpper() == "REJECTED"))
            {
                try
                {
                    _loanApplicationService.ProcessLoanApplication(applicationId, approvalStatus.ToUpper());
                    ViewBag.ProcessMessage = "Application status updated successfully.";
                    return RedirectToAction("Process", new { applicationId = applicationId });
                }
                catch (ArgumentException ex)
                {
                    ViewBag.ProcessErrorMessage = ex.Message;
                    return View("Process", _loanApplicationService.GetById(applicationId));
                }
                catch (InvalidOperationException ex)
                {
                    ViewBag.ProcessErrorMessage = ex.Message;
                    return View("Process", _loanApplicationService.GetById(applicationId));
                }
                catch (Exception ex)
                {
                    ViewBag.ProcessErrorMessage = "An unexpected error occurred while processing the application.";
                    return View("Process", _loanApplicationService.GetById(applicationId));
                }
            }
            else
            {
                ViewBag.ProcessErrorMessage = "Invalid approval status. Please select 'APPROVED' or 'REJECTED'.";
                return View("Process", _loanApplicationService.GetById(applicationId));
            }
        }

        [HttpGet("PendingApplications")] // Route for listing pending applications
        public IActionResult PendingApplications()
        {
            try
            {
                // Assuming you have a method in your service to get all applications
                IEnumerable<LoanApplicationEntites> pendingApplications = _loanApplicationService.GetAllApplications().Where(a => a.ApprovalStatus == "PENDING");
                return View(pendingApplications); // Pass the list to the view
            }
            catch (Exception ex)
            {
                // Log the error!
                ViewBag.ErrorMessage = "An error occurred while retrieving pending applications.";
                return View(new List<LoanApplicationEntites>()); // Return an empty list to avoid view errors
            }
        }
    }
}
    
