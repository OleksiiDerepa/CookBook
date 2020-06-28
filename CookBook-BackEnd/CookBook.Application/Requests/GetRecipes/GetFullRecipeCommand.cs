using System;
using CookBook.Application.Models;
using MediatR;

namespace CookBook.Application.Requests.GetRecipes
{
    public class GetFullRecipeCommand 
        : IRequest<RecipeFullModel>
    {
        public Guid RecipeId { get; set; }
    }
}