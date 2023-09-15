using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UdemyCrudApp1.Data;
using UdemyCrudApp1.Models;

namespace UdemyCrudApp1.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpenseController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Display list of Expenses
        public IActionResult Index()
        {
            IEnumerable<Expense> ExpList = _context.Expenses;
            return View(ExpList);
        }

        //Display Expenses add Form
        public IActionResult Create() {
            
            //DropDown list for Category type
            IEnumerable<SelectListItem> CategoryDropDown = _context.Categories.Select(i => new SelectListItem { 
                Text = i.CategoryName,
                Value = i.Id.ToString()
            });

            ViewBag.CategoryDropDown = CategoryDropDown;
            
            return View();
        }

        //POST Create Expenses
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Expense obj)
        {
            if (!ModelState.IsValid)
            {
                _context.Expenses.Add(obj);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _context.Expenses.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //POST Delete Expenses
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _context.Expenses.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
           
            _context.Expenses.Remove(obj);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET Update
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _context.Expenses.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //POST Update Expenses
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Expense obj)
        {
            if (ModelState.IsValid)
            {
                _context.Expenses.Update(obj);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
