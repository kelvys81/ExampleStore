using APIStore.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIStore.Filters.ExecptionFilters
{
    public class Product_HandleUpdateExceptionsFilterAttribute : ExceptionFilterAttribute
    {

        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
            var strProdId = context.RouteData.Values["id"] as string;
            if (int.TryParse(strProdId, out int prodId))
            {
                if (!ProductRepository.ProductExists(prodId))
                {
                    context.ModelState.AddModelError("ProdId", "Product doesn't existg anymore.");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status404NotFound
                    };
                    context.Result = new NotFoundObjectResult(problemDetails); 
                }
            }
        }

    }
}
