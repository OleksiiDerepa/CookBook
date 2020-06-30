using System;
using System.Threading.Tasks;
using CookBook.Application.Models;
using CookBook.Application.Queries;
using CookBook.Application.Requests.CreateUpdateRecipe;
using CookBook.Application.Requests.GetParentHierarchy;
using CookBook.Application.Requests.GetRecipes;
using CookBook.Infrastructure.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CookBook.API.Controllers
{
    [Route("api/recipe")]
    public class RecipeController : ApplicationController
    {
        public RecipeController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Create recipe
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<string>> CreateRecipeAsync([BindRequired, FromBody] CreateRecipeModel model)
        {
            var result = await Mediator.Send(new CreateRecipeCommand {RecipeModel = model});

            return Item(result);
        }

        /// <summary>
        /// Update recipe
        /// </summary>
        /// <param name="recipeId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{recipeId}")]
        public async Task<ActionResult<SuccessResultViewModel>> UpdateRecipeAsync(
            [BindRequired, FromRoute] Guid recipeId,
            [BindRequired, FromBody] UpdateRecipeModel model)
        {
            var result = await Mediator.Send(new UpdateRecipeCommand
            {
                recipeId = recipeId,
                RecipeModel = model
            });

            return Item(result);
        }

        /// <summary>
        /// Get parent hierarchy by recipe id
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        [HttpGet("parent-hierarchy/{recipeId}")]
        public async Task<ActionResult<CollectionApiResponse<RecipeShortModel>>> GetParentHierarchyAsync([BindRequired, FromRoute] Guid recipeId)
        {
            var result = await Mediator.Send(new GetParentHierarchyCommand {RecipeId = recipeId});

            return Ok(result);
        }

        /// <summary>
        /// Get recipe by id
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        [HttpGet("{recipeId}")]
        public async Task<ActionResult<RecipeFullModel>> GetRecipeByIdAsync([BindRequired, FromRoute] Guid recipeId)
        {
            var result = await Mediator.Send(new GetFullRecipeCommand {RecipeId = recipeId});

            return Item(result);
        }

        /// <summary>
        /// Get recipe short model list
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<CollectionApiResponse<RecipeShortModel>>> GetRecipesAsync(
            [FromQuery] ApiQueryRequest request)
        {
            var result = await Mediator.Send(new GetRecipeListCommand {Request = request});

            return Ok(result);
        }
    }
}