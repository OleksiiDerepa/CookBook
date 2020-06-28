using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CookBook.Application.Queries
{
    public class ApiQueryRequest : IValidatableObject
    {
        public const int DefaultPageSize = 10;
        public const int MaxPageSize = 100;

        [Range(1, int.MaxValue)]
        public int Page { get; set; }

        [Range(1, MaxPageSize)]
        public int Size { get; set; }
        
        public Guid? ParentId { get; set; }

        public ApiQueryRequest()
        {
            Page = 1;
            Size = DefaultPageSize;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ParentId != null && ParentId == Guid.Empty)
            {
                yield return new ValidationResult(
                    "Recipe Id should not be empty",
                    new[] { nameof(ParentId) });
            }
        }
    }
}