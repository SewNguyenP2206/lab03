using BussinessObjects.DTOs;
using BussinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.Interfaces;

namespace ProjectManagementAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        
        private IProductRepository repository = new ProductRepository();


        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts() => repository.GetProducts();

        // POST: ProductsController/Products
        [HttpPost]
        public IActionResult PostProduct(ProductCreateRequest p)
        {
            Product product = new Product
            {
                UnitsInStock = p.UnitsInStock,
                CategoryId = p.CategoryId,
                UnitPrice = p.UnitPrice,
                ProductName = p.ProductName
            };
            repository.SaveProduct(product);
            return NoContent();
        }

        // DELETE: ProductsController/Delete/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var p = repository.GetProductById(id);
            if (p == null)
            {
                return NotFound();
            }

            repository.DeleteProduct(p);
            return NoContent();
        }

        // PUT: ProductsController/Update/5
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, Product p)
        {
            var productInDb = repository.GetProductById(id);
            if (productInDb == null)
            {
                return NotFound();
            }

            // Update the product in the database with the values from the request
            productInDb.ProductName = p.ProductName;
            productInDb.UnitPrice = p.UnitPrice;
            productInDb.CategoryId = p.CategoryId;
            productInDb.UnitsInStock = p.UnitsInStock;

            repository.UpdateProduct(productInDb);
            return NoContent();
        }


    }
}
