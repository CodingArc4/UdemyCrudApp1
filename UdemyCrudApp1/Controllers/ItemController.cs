using Microsoft.AspNetCore.Mvc;
using UdemyCrudApp1.Data;
using UdemyCrudApp1.Models;

namespace UdemyCrudApp1.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        //pushing dependency to use databse services
        public ItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        //action result to show data in index page
        public IActionResult Index()
        {
            IEnumerable<Item> objList = _context.Items;
            return View(objList);
        }

        //action result to display the create page whenever a user click on create new item button
        //GET-Create
        public IActionResult Create()
        {
            return View();
        }

        //POST-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Item obj)
        {
            _context.Items.Add(obj);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
