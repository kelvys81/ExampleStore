using APIStore.Filters.ActionFilters;
using APIStore.Filters.ExecptionFilters;
using APIStore.Models;
using APIStore.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace APIStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProducts() 
        { 
            return Ok(ProductRepository.GetProducts());
        }

        [HttpGet("{id}")]
        [Product_ValidateProductIdFilter]
        public IActionResult GetProductById(int id) 
        { 
            return Ok(ProductRepository.GetProductByID(id)); 
        }

        [HttpPost]
        [Product_ValidateCreateProductFilter]
        public IActionResult CreateProduct([FromBody] Product prod) 
        {
            ProductRepository.AddProduct(prod);
            return CreatedAtAction(nameof(GetProductById), new { id = prod.ProdId }, prod);
        }

        [HttpPut("{id}")]
        [Product_ValidateUpdateProdFilter]
        [Product_HandleUpdateExceptionsFilter]
        public IActionResult UpdateProduct(int id, Product prod)
        {
            ProductRepository.UpdateProduct(prod);
            return NoContent();
        }


        [HttpDelete("{id}")]
        [Product_ValidateProductIdFilter]
        public IActionResult DeleteProduct(int id)
        {
            var prod = ProductRepository.GetProductByID(id);
            ProductRepository.DeleteProduct(id);

            return Ok(prod);
        }



    }
}
