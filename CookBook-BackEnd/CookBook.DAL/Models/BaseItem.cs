using System;
using System.ComponentModel.DataAnnotations;

namespace CookBook.DAL.Models
{
    public class BaseItem
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ModifiedAt { get; set; }
    }
}