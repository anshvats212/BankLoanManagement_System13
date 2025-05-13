// C:\bankLoanmanagement133copy\BankLoan_Management133\BankLoan_Management133\Controllers\CustomerController.cs
using Microsoft.AspNetCore.Mvc;
using BankLoan_Management133.Models;
using BankLoan_Management133.BusinessLogic;
using BankLoan_Management133.Repositoryy.Models;
using Microsoft.AspNetCore.Identity;
using System.Numerics;

namespace BankLoan_Management133.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IBusinessLogic _businessLogic;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public CustomerController(IBusinessLogic businessLogic, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _businessLogic = businessLogic;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Registration Actions
        public IActionResult Index()
        {
            return View(); // Now the view expects RegisterViewModel, and we're passing an empty one (implicitly)
        }

        [HttpPost]
        public IActionResult Index(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = _userManager.CreateAsync(user, model.Password).Result; // Synchronous call

                if (result.Succeeded)
                {
                    // After successful Identity user creation, create the customer profile
                    var customer = new customer
                    {
                        Email = model.Email,
                        Name = model.Name,
                        Phone = model.Phone,
                        Address = model.Address,
                        KycStatus = model.Kyc // Set initial KYC status
                    };
                    _businessLogic.SaveCustomer(customer); // Use your existing logic to save customer details

                    // Sign in the user after registration
                    _signInManager.SignInAsync(user, isPersistent: false).Wait(); // Synchronous call

                    // Redirect to the Dashboard after successful registration and sign-in
                    return RedirectToAction("Login", new { customerid = _businessLogic.GetCustomerByEmail(model.Email)?.CustomerId });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            // If ModelState is not valid or registration fails, return the view with the model
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var result = _signInManager.PasswordSignInAsync(email, password, isPersistent: false, lockoutOnFailure: false).Result; // Synchronous call

                if (result.Succeeded)
                {
                    // Redirect to the dashboard or another appropriate page upon successful login
                    return RedirectToAction("Dashboard", new { customerid = _businessLogic.GetCustomerByEmail(email)?.CustomerId });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(); // Return to the login form with an error message
                }
            }
            // If ModelState is not valid, return the login form with validation errors
            return View();
        }


        public IActionResult Dashboard(int? customerid) // Receive customerid as an integer (nullable)
        {
            if (customerid == null)
            {
                return NotFound(); // Or handle the case where customerid is missing
            }

            var customer = _businessLogic.GetCustomerById(customerid.Value); // Fetch customer by ID

            if (customer == null)
            {
                return NotFound(); // Or handle the case where the customer is not found
            }

            ViewData["Title"] = "Customer Dashboard";
            return View(customer); // Pass the fetched customer object to the view
        }

        public IActionResult Edit(int? customerId)
        {
            if (customerId == null)
            {
                return NotFound();
            }

            var customer = _businessLogic.GetCustomerById(customerId.Value);

            if (customer == null)
            {
                return NotFound();
            }

            ViewBag.st = "Update"; // Set ViewBag for the button text in the view
            return View(customer);
        }

        // POST: Customer/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(customer model)
        {
            if (ModelState.IsValid)
            {
                _businessLogic.UpdateCustomerProfile(model);
                return RedirectToAction("Dashboard", new { customerid = model.CustomerId });
            }
            ViewBag.st = "Update";
            return View(model); // Explicitly return the Edit view on validation failure
        }


        public IActionResult Show()
        {
            var data = _businessLogic.GetAllCustomers();
            return View(data); ;

            // ... other actions ...
        }
    }
}