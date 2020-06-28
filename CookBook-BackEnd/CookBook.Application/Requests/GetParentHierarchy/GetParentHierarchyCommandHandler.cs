using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CookBook.Application.Models;
using CookBook.DAL;
using CookBook.DAL.Models;
using CookBook.Infrastructure.Responses;
using CookBook.Infrastructure.Validation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Application.Requests.GetParentHierarchy
{
    public class GetParentHierarchyCommandHandler
        : IRequestHandler<GetParentHierarchyCommand, CollectionApiResponse<RecipeShortModel>>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GetParentHierarchyCommandHandler(ApplicationDbContext applicationDbContext)
        {
            Require.Objects.NotNull(applicationDbContext, nameof(applicationDbContext));

            _applicationDbContext = applicationDbContext;
        }

        public async Task<CollectionApiResponse<RecipeShortModel>> Handle(
            GetParentHierarchyCommand request,
            CancellationToken cancellationToken)
        {
            var recipe = await _applicationDbContext.Set<Recipe>()
                .SingleOrDefaultAsync(x => x.Id == request.RecipeId.ToString(), cancellationToken);
            
            CollectionApiResponse<RecipeShortModel> collectionApiResponse;
            if (recipe.HierarchyPath?.Split("/") is { } hierarchy)
            {
                var result =
                    await _applicationDbContext.Set<Recipe>()
                        .Where(x => hierarchy.Any(h => x.Id == h))
                        .Select(r => new RecipeShortModel
                            {
                                Id = r.Id,
                                Title = r.Title,
                                CreatedAt = r.CreatedAt,
                                ParentId = r.ParentId
                            })
                        .ToArrayAsync(cancellationToken);
                
                result = hierarchy
                    .Join(result, 
                        a => a, 
                        b => b.Id, 
                        (a, b) => b)
                    .ToArray();
                
                collectionApiResponse = GetCollectionApiResponse(result);
                
                return collectionApiResponse;
            }

            var recipeShortModel = new RecipeShortModel
            {
                Id = recipe.Id,
                Title = recipe.Title,
                CreatedAt = recipe.CreatedAt,
                ParentId = recipe.ParentId
            };
            
            collectionApiResponse = GetCollectionApiResponse(recipeShortModel);
            
            return collectionApiResponse;
        }

        private static CollectionApiResponse<RecipeShortModel> GetCollectionApiResponse(params RecipeShortModel[] result)
        {
            CollectionApiResponse<RecipeShortModel> collectionApiResponse =
                new CollectionApiResponse<RecipeShortModel>
                {
                    Content = result,
                    CurrentPage = 1,
                    OverallCount = result.Length,
                    PageCount = 1,
                    PageSize = result.Length
                };
            
            return collectionApiResponse;
        }
    }
}