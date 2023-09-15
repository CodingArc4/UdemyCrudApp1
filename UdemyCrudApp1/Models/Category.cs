using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UdemyCrudApp1.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string CategoryName { get; set; }
    }
}
