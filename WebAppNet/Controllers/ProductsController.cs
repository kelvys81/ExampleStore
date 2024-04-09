using Microsoft.AspNetCore.Mvc;
using WebAppNet.Data;
using WebAppNet.Models;

namespace WebAppNet.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IWebApiExecuter webApiExecuter;

        public ProductsController(IWebApiExecuter webApiExecuter)
        {
            this.webApiExecuter = webApiExecuter;
        }
        public async Task<IActionResult> Index()
        {
            return View(await webApiExecuter.InvokeGet<List<Product>>("products"));
        }
        public IActionResult CreateProduct()
        { 
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product prod)
        {
            try 
            {
                var response = await webApiExecuter.InvokePost("products", prod);
                if (response != null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (WebApiException ex) 
            {
                HandleWebApiExeption(ex);
            }
            return View(prod);
        }
        public async Task<IActionResult> UpdateProduct(int prodId)
        {
            try 
            {
                var prod = await webApiExecuter.InvokeGet<Product>($"products/{prodId}");
                if (prod != null)
                {
                    return View(prod);
                }
            }
            catch(WebApiException ex)
            {
                HandleWebApiExeption(ex);
                return View();
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(Product prod)
        {
            if (ModelState.IsValid)
            {
                try 
                {
                    await webApiExecuter.InvokePut($"products/{prod.ProdId}", prod);
                    return RedirectToAction(nameof(Index));
                }
                catch(WebApiException ex) 
                {
                    HandleWebApiExeption(ex);
                }
            }
            return View(prod);
        }

        public async Task<IActionResult> DeleteProduct(int prodId)
        {
            try 
            {
                await webApiExecuter.InvokeDelete($"products/{prodId}");
                return RedirectToAction(nameof(Index));
            }
            catch (WebApiException ex) 
            {
                HandleWebApiExeption(ex);
                return View(nameof(Index),
                    await webApiExecuter.InvokeGet<List<Product>>("products"));
            }
        }

        private void HandleWebApiExeption(WebApiException ex) 
        {
            if (ex.ErrorResponse != null &&
                    ex.ErrorResponse.Errors != null &&
                    ex.ErrorResponse.Errors.Count > 0)
            {
                foreach (var error in ex.ErrorResponse.Errors)
                {
                    ModelState.AddModelError(error.Key, string.Join(";", error.Value));
                }
            }
        }
    }
}
