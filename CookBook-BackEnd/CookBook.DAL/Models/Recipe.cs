using System.Collections;
using System.Collections.Generic;

namespace CookBook.DAL.Models
{
    public class Recipe : BaseItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ParentId { get; set; }
        public Recipe Parent { get; set; }
        public ICollection<Recipe> Children { get; set; }
        
        public string HierarchyPath { get; set; }
    }
}