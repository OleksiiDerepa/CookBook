using System;
using System.Threading;
using System.Threading.Tasks;
using CookBook.Application.Models;
using CookBook.DAL;
using CookBook.DAL.Models;
using CookBook.Infrastructure.Validation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Application.Requests.CreateUpdateRecipe
{
    public class CreateUpdateRecipeCommandHandler
        : IRequestHandler<CreateRecipeCommand, string>,
            IRequestHandler<UpdateRecipeCommand, SuccessResultViewModel>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CreateUpdateRecipeCommandHandler(ApplicationDbContext applicationDbContext)
        {
            Require.Objects.NotNull(applicationDbContext, nameof(applicationDbContext));

            _applicationDbContext = applicationDbContext;
        }

        public async Task<string> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
        {
            Recipe parent = null;

            if (request.RecipeModel.ParentId != null)
            {
                parent = await TryGetRecipeAsync(request.RecipeModel.ParentId?.ToString(), cancellationToken);
            }

            var path = CombinePath(parent);
                
            Recipe recipe = new Recipe
            {
                Title = request.RecipeModel.Title,
                Parent = parent,
                Description = request.RecipeModel.Description,
                HierarchyPath = path
            };

            _applicationDbContext.Set<Recipe>().Add(recipe);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return recipe.Id;
        }

        private static string CombinePath(Recipe parent)
        {
            string path = parent == null
                ? null
                : parent.HierarchyPath == null
                    ? parent.Id
                    : string.Join("/", parent.HierarchyPath, parent.Id);
            return path;
        }

        public async Task<SuccessResultViewModel> Handle(
            UpdateRecipeCommand request,
            CancellationToken cancellationToken)
        {
            var recipe = await TryGetRecipeAsync(request.recipeId.ToString(), cancellationToken);

            recipe.ModifiedAt = DateTime.Now;
            recipe.Title = request.RecipeModel.Title;
            recipe.Description = request.RecipeModel.Description;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new SuccessResultViewModel();
        }

        private async Task<Recipe> TryGetRecipeAsync(string recipeId, CancellationToken cancellationToken)
        {
            var recipe = await _applicationDbContext.Set<Recipe>()
                .SingleOrDefaultAsync(x => x.Id == recipeId, cancellationToken);
            
            Require.Objects.NotNull(recipe, "Specified recipe does not exist");

            return recipe;
        }
    }
}