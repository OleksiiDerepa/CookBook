namespace CookBook.Application.Models
{
    public class RecipeFullModel
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
        public string HierarchyPath { get; set; }
    }
}