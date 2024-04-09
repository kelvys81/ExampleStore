using APIStore.Models;
using APIStore.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIStore.Filters.ActionFilters
{
    public class Product_ValidateCreateProductFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var prod = context.ActionArguments["prod"] as Product;
            if (prod == null)
            {
                context.ModelState.AddModelError("Product", "Object is null.");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
            else
            {
                var existingProd = ProductRepository.GetProductByProperties(prod.Name,prod.Brand,prod.Gender,prod.Color,prod.Size);

                if (existingProd != null)
                {
                    context.ModelState.AddModelError("Product", "Product already exists.");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
            }
        }

        //
    }
}
