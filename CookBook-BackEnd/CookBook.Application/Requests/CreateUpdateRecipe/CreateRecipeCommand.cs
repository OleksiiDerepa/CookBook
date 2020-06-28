using CookBook.Application.Models;
using MediatR;

namespace CookBook.Application.Requests.CreateUpdateRecipe
{
    public class CreateRecipeCommand : IRequest<string>
    {
        public CreateRecipeModel RecipeModel { get; set; }
    }
}