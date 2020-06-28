using System;

namespace CookBook.Application.Models
{
    public class RecipeShortModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ParentId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}