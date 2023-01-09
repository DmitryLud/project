using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.DataAccess;

namespace WebApplication1.Controllers
{
    [Route("/api/[controller]")]
    public class ProductsController : Controller
    {
        DataBaseAccess db = new DataBaseAccess();

        [HttpGet]
        public async Task<List<Product>> Get() => await db.GetAllProducts();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Product>> Get(string id)
        {
            var product = await db.GetProduct(id);

            if (product is null) return NotFound();

            return product;
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var product = await db.GetProduct(id);

            if (product is null) return NotFound();

            await db.RemoveProduct(id);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await db.CreateProduct(product);
            return NoContent();
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Product updatedProduct)
        {
            var product = await db.GetProduct(id);

            if (product is null) return NotFound();

            updatedProduct.Id = product.Id;

            await db.UpdateProduct(updatedProduct, id);

            return Ok();
        }

    }
}
