using System.ComponentModel.DataAnnotations;

namespace CookBook.Application.Models
{
    public class CreateUpdateRecipeModel
    {
        [StringLength(150)]
        public string Title { get; set; }
        
        [StringLength(1000)]
        public string Description { get; set; }
    }
}