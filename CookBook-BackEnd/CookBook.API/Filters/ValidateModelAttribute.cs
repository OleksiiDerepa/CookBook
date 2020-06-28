using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using Newtonsoft.Json;

namespace CookBook.API.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var response = context.HttpContext.Response;
                var errors = SerializeErrors(context.ModelState);

                response.StatusCode = (int)HttpStatusCode.BadRequest;

                context.Result = new BadRequestObjectResult(errors);
            }
        }

        private static Dictionary<string, string[]> SerializeErrors(ModelStateDictionary modelState)
        {
            Dictionary<string, string[]> result = new Dictionary<string, string[]>();

            foreach (var keyModelStatePair in modelState)
            {
                var key = keyModelStatePair.Key;
                var errors = keyModelStatePair.Value.Errors;
                if (errors != null && errors.Count > 0)
                {
                    var errorMessages = errors.Select(GetErrorMessage)
                        .Distinct()
                        .ToArray();

                    result.Add(key, errorMessages);
                }
            }

            return result;
        }

        private static string GetErrorMessage(ModelError modelError)
        {
            if (!string.IsNullOrEmpty(modelError.ErrorMessage))
            {
                return modelError.ErrorMessage;
            }

            if (modelError.Exception is JsonSerializationException)
            {
                return modelError.Exception.Message;
            }

            return modelError.Exception is JsonException
                ? "Request body has invalid JSON format."
                : "Passed parameter is invalid.";
        }
    }}