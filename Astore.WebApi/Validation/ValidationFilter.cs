using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Astore.WebApi.Validation;

public class ValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage))
                .ToArray();

            var errorResponse = new ErrorResponse();
            foreach (var error in errors)
            foreach (var subError in error.Value)
            {
                var errorModel = new ErrorModel
                {
                    Field = error.Key,
                    Message = subError
                };
                errorResponse.Errors.Add(errorModel);
            }

            context.Result = new BadRequestObjectResult(errorResponse);
            return;
        }

        await next();
    }
}