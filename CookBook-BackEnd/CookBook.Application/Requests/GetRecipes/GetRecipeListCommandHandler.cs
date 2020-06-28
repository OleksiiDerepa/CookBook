using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CookBook.Application.Extensions;
using CookBook.Application.Models;
using CookBook.Application.Queries;
using CookBook.DAL;
using CookBook.DAL.Models;
using CookBook.Infrastructure.Responses;
using CookBook.Infrastructure.Validation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CookBook.Infrastructure.Exceptions;

namespace CookBook.Application.Requests.GetRecipes
{
    public class GetRecipeListCommandHandler
        : IRequestHandler<GetRecipeListCommand, CollectionApiResponse<RecipeShortModel>>,
            IRequestHandler<GetFullRecipeCommand, RecipeFullModel>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GetRecipeListCommandHandler(ApplicationDbContext applicationDbContext)
        {
            Require.Objects.NotNull(applicationDbContext, nameof(applicationDbContext));
            
            _applicationDbContext = applicationDbContext;
        }

        public async Task<CollectionApiResponse<RecipeShortModel>> Handle(
            GetRecipeListCommand request,
            CancellationToken cancellationToken)
        {
            var result = await GetRecipeShortModels(request.Request, cancellationToken);

            return result;
        }

        private async Task<CollectionApiResponse<RecipeShortModel>> GetRecipeShortModels(
            ApiQueryRequest request,
            CancellationToken cancellationToken)
        {
            var parentId = request.ParentId?.ToString();

            var page = await _applicationDbContext.Set<Recipe>()
                .Where(x => x.ParentId == parentId)
                .OrderBy(x => x.Title)
                .Select(x => new RecipeShortModel
                {
                    Id = x.Id,
                    ParentId = x.ParentId,
                    Title = x.Title,
                    CreatedAt = x.CreatedAt
                })
                .GetPageAsync(request, cancellationToken);

            return page;
        }

        public async Task<RecipeFullModel> Handle(
            GetFullRecipeCommand request,
            CancellationToken cancellationToken)
        {
            var recipeId = request.RecipeId.ToString();
            var recipe = await _applicationDbContext.Set<Recipe>()
                .SingleOrDefaultAsync(x => x.Id == recipeId, cancellationToken);
			
            Require.Objects.NotNull<NotFoundException>(recipe, "specified recipe does not exist");
			
            var recipeModel = new RecipeFullModel
            {
                Id = recipe.Id,           
                ParentId = recipe.ParentId,
                Title = recipe.Title,
                Description = recipe.Description,
                HierarchyPath = recipe.HierarchyPath
            };

            return recipeModel;
        }
    }
}