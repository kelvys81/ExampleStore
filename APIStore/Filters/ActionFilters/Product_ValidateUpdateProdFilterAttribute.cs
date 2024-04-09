using APIStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIStore.Filters.ActionFilters
{
    public class Product_ValidateUpdateProdFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var id = context.ActionArguments["id"] as int?;
            var prod = context.ActionArguments["prod"] as Product;
            if (id.HasValue && prod != null && id != prod.ProdId)
            {
                context.ModelState.AddModelError("ProdId", "ProductId is not the same as id.");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
        }

    }
}
