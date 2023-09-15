using Microsoft.AspNetCore.Mvc.Rendering;

namespace UdemyCrudApp1.Models.ViewModels
{
    public class ExpenseVM
    {
        public Expense Expense { get; set; }
        public IEnumerable<SelectListItem>? CategoryDropDown { get; set; }
    }
}
