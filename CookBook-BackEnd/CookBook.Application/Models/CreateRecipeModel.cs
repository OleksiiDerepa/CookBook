using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CookBook.Application.Models
{
    public class CreateRecipeModel : CreateUpdateRecipeModel, IValidatableObject
    {
        public Guid? ParentId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ParentId != null && ParentId == Guid.Empty)
            {
                yield return new ValidationResult(
                    "Recipe Id should not be empty",
                    new[] { nameof(ParentId) });
            }
        }}
}