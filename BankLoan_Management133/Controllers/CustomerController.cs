using Microsoft.AspNetCore.Mvc;
using BankLoan_Management133.Models;
using BankLoan_Management133.BusinessLogic;
using BankLoan_Management133.Repositoryy.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;

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
            return View();
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

                    // Sign in the user and add CustomerId as a claim
                    SignInAndAddCustomerIdClaim(user, customer.CustomerId, false);

                    // Redirect to the Dashboard after successful registration and sign-in
                    return RedirectToAction("Dashboard");
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
                    var user = _userManager.FindByEmailAsync(email).Result; // Synchronous call
                    if (user != null)
                    {
                        var customer = _businessLogic.GetCustomerByEmail(email);
                        if (customer != null)
                        {
                            // Ensure the CustomerId claim is present after login
                            var existingCustomerIdClaim = _userManager.GetClaimsAsync(user).Result; // Synchronous call
                            if (!existingCustomerIdClaim.Any(c => c.Type == "CustomerId"))
                            {
                                AddCustomerIdClaim(user, customer.CustomerId);
                            }
                            return RedirectToAction("Dashboard");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Customer profile not found for this user.");
                            return View();
                        }
                    }
                    return RedirectToAction("Dashboard"); // Should not reach here if user is null
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

        public IActionResult Dashboard()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return RedirectToAction("Login"); // Or handle unauthenticated access
            }

            var user = _userManager.FindByIdAsync(userId).Result; // Synchronous call
            if (user == null)
            {
                return RedirectToAction("Login"); // Handle user not found
            }

            var customerIdClaim = User.Claims.FirstOrDefault(c => c.Type == "CustomerId")?.Value;

            if (string.IsNullOrEmpty(customerIdClaim) || !int.TryParse(customerIdClaim, out int customerId))
            {
                // This should ideally not happen if the claim was added during login/registration
                // Log an error or redirect to an error page
                return RedirectToAction("Login"); // Or handle the error appropriately
            }

            var customer = _businessLogic.GetCustomerById(customerId);
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
                return RedirectToAction("Dashboard"); // Redirect to dashboard, ID is in claims now
            }
            ViewBag.st = "Update";
            return View(model); // Explicitly return the Edit view on validation failure
        }


        public IActionResult Show()
        {
            var data = _businessLogic.GetAllCustomers();
            return View(data); ;
        }

        private void SignInAndAddCustomerIdClaim(IdentityUser user, int customerId, bool isPersistent)
        {
            _signInManager.SignInAsync(user, isPersistent).Wait(); // Synchronous call
            AddCustomerIdClaim(user, customerId);
        }

        private void AddCustomerIdClaim(IdentityUser user, int customerId)
        {
            var claim = new Claim("CustomerId", customerId.ToString());
            _userManager.AddClaimAsync(user, claim).Wait(); // Synchronous call
        }

        [HttpPost]
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync().Wait(); // Synchronous call
            return RedirectToAction("Login"); // Redirect to the login page after logout
        }

        // You might also have a GET action for logout if you prefer a link
        public IActionResult LogoutGet()
        {
            _signInManager.SignOutAsync().Wait(); // Synchronous call
            return RedirectToAction("Login"); // Redirect to the login page after logout
        }
    }
}