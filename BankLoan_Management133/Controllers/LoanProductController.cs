using BankLoan_Management133.BusinessLogicc.Interfaces;
using BankLoan_Management133.Repositoryy.Models;
using Microsoft.AspNetCore.Mvc;
 namespace BankLoan_Management133.Controllers
{
    public class LoanProductController : Controller
    {
        private readonly ILoanProductService _loanProductService;

        public LoanProductController(ILoanProductService loanProductService)
        {
            _loanProductService = loanProductService;
        }

        public IActionResult Index()
        {
            // Use the fully qualified type for the List declaration
            List<LoanProduct> loanProducts = _loanProductService.GetAllLoanProducts().ToList();
            return View(loanProducts); // Pass the list of the correct type to the view
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Good practice
        // Use the fully qualified type for the parameter
        public IActionResult Create(LoanProduct obj)
        {
            // Model binding expects this type
            if (ModelState.IsValid)
            {
                // Pass the object of the correct type to the service
                _loanProductService.CreateLoanProduct(obj);
                TempData["success"] = "Added Successfully🙌😁";
                return RedirectToAction("Index");
            }
            // Pass the object of the correct type back to the view
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // Use the fully qualified type for the variable declaration
            LoanProduct? productsearchfromdb = _loanProductService.GetLoanProductById(id.Value);

            if (productsearchfromdb == null)
            {
                return NotFound();
            }

            // Pass the object of the correct type to the view
            return View(productsearchfromdb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Good practice
        // Use the fully qualified type for the parameter
        public IActionResult Edit(LoanProduct obj)
        {
            // Model binding expects this type
            if (ModelState.IsValid)
            {
                // Pass the object of the correct type to the service
                _loanProductService.UpdateLoanProduct(obj);
                TempData["success"] = "Updated Successfully🙌";
                return RedirectToAction("Index");
            }
            // Pass the object of the correct type back to the view
            return View(obj);
        }

        public IActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            // Use the fully qualified type for the variable declaration
            LoanProduct? obj = _loanProductService.GetLoanProductById(id.Value);

            if (obj == null)
            {
                return NotFound();
            }

            // Pass the object of the correct type to the view
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            // Use the fully qualified type for the variable declaration
            LoanProduct? takingfromdb = _loanProductService.GetLoanProductById(id.Value);
            if (takingfromdb == null)
            {
                return NotFound();
            }
            // Pass the object of the correct type to the view
            return View(takingfromdb);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] // Good practice
        public IActionResult DeletePost(int id)
        {
            // The service handles finding the item first before deleting
            _loanProductService.DeleteLoanProduct(id);

            TempData["success"] = "Deleted Successfully🙌";
            return RedirectToAction("Index");
        }
    }
}
