using System;
using CookBook.Application.Models;
using MediatR;

namespace CookBook.Application.Requests.CreateUpdateRecipe
{
    public class UpdateRecipeCommand : IRequest<SuccessResultViewModel>
    {
        public Guid recipeId { get; set; }
        public UpdateRecipeModel RecipeModel { get; set; }
    }
}