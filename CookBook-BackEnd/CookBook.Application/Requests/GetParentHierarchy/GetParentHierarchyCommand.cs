using System;
using CookBook.Application.Models;
using CookBook.Infrastructure.Responses;
using MediatR;

namespace CookBook.Application.Requests.GetParentHierarchy
{
    public class GetParentHierarchyCommand
        : IRequest<CollectionApiResponse<RecipeShortModel>>
    {
        public Guid RecipeId { get; set; }
    }
}