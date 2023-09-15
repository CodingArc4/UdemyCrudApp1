using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UdemyCrudApp1.Data;
using UdemyCrudApp1.Models;
using UdemyCrudApp1.Models.ViewModels;

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
            IEnumerable<Expense> ExpList = _context.Expenses.Include(e => e.Category).ToList();

            //foreach (var item in ExpList)
            //{
            //    item.Category = _context.Categories.FirstOrDefault(i => i.Id == item.CategoryId);
            //}

            return View(ExpList);
        }

        //Display Expenses add Form
        public IActionResult Create() {

            //DropDown list for Category type
            //IEnumerable<SelectListItem> CategoryDropDown = _context.Categories.Select(i => new SelectListItem { 
            //    Text = i.CategoryName,
            //    Value = i.Id.ToString()
            //});

            //ViewBag.CategoryDropDown = CategoryDropDown;

            
            ExpenseVM expenseVM = new ExpenseVM()
            {
                Expense = new Expense(),
                CategoryDropDown = _context.Categories.Select(i => new SelectListItem
                {
                        Text = i.CategoryName,
                        Value = i.Id.ToString()
                })
            };

            return View(expenseVM);
        }

        //POST Create Expenses
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExpenseVM obj)
        {
            if (ModelState.IsValid)
            {
                _context.Expenses.Add(obj.Expense);
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
            ExpenseVM expenseVM = new ExpenseVM()
            {
                Expense = new Expense(),
                CategoryDropDown = _context.Categories.Select(i => new SelectListItem
                {
                    Text = i.CategoryName,
                    Value = i.Id.ToString()
                })
            };

            if (id == null || id == 0)
            {
                return NotFound();
            }

            expenseVM.Expense = _context.Expenses.Find(id);
            if (expenseVM.Expense == null)
            {
                return NotFound();
            }
            return View(expenseVM);
        }

        //POST Update Expenses
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ExpenseVM obj)
        {
            if (ModelState.IsValid)
            {
                _context.Expenses.Update(obj.Expense);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
