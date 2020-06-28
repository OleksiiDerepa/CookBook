using System.Collections.Generic;
using CookBook.Application.Models;
using CookBook.Application.Queries;
using CookBook.Infrastructure.Responses;
using MediatR;

namespace CookBook.Application.Requests.GetRecipes
{
    public class GetRecipeListCommand 
        : IRequest<CollectionApiResponse<RecipeShortModel>>
    {
        public ApiQueryRequest Request { get; set; }
    }
}