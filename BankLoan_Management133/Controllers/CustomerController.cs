using Microsoft.AspNetCore.Mvc;
using BankLoan_Management133.Models;
using BankLoan_Management133.BusinessLogic;
using BankLoan_Management133.Repositoryy.Models;
using System.Numerics;


namespace BankLoan_Management133.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IBusinessLogic _businessLogic;

        public CustomerController(IBusinessLogic businessLogic)
        {
            _businessLogic = businessLogic;
        }

        public IActionResult Index(int id = 0)
        {
            customer obj = new customer();
            ViewBag.st = "Submit";
            if (id > 0)
            {
                obj = _businessLogic.GetCustomerById(id);
                ViewBag.st = "Update";
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Index(customer obj)
        {
            _businessLogic.SaveCustomer(obj);
            return RedirectToAction("Login");
        }

        public IActionResult Show()
        {
            var data = _businessLogic.GetAllCustomers();
            return View(data);
        }
        public IActionResult Delete(int id = 0)
        {
            _businessLogic.DeleteCustomer(id);
            return RedirectToAction("Show");
        }


        public IActionResult Login(string email, string password)

        {

            if (_businessLogic.Login(email, password))

            {

                // In a real application, you'd use authentication cookies

                var data = _businessLogic.GetCustomerByEmail(email);

                if (data != null)

                {

                    return RedirectToAction("Dashboard", new { Customerid = data.CustomerId });

                }

            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");

            return View();

        }

        public IActionResult Dashboard(int customerid)
        {
            var data = _businessLogic.GetCustomerById(customerid);
            if (data == null) // Check if data is null (customer not found)
            {
                return NotFound();
            }
            return View(data); // Return the view with customer data if found
        }

        public IActionResult Edit(int customerId)
        {
            var data = _businessLogic.GetCustomerById(customerId);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(customer model)
        {
            if (ModelState.IsValid)
            {
                _businessLogic.UpdateCustomerProfile(model);
                return RedirectToAction("Dashboard", new { customerId = model.CustomerId });
            }
            return View(model);
        }
    }
}

