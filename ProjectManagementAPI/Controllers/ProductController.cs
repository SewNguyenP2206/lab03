using BussinessObjects.DTOs;
using BussinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using System.Collections.Generic;
using UnitOfWorks.Interfaces;

namespace ProjectManagementAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var products = _unitOfWork.Products.GetAll(); 
            return Ok(products);
        }

        [HttpPost]
        public IActionResult PostProduct(ProductCreateRequest request)
        {
            Product product = new Product
            {
                UnitsInStock = request.UnitsInStock,
                CategoryId = request.CategoryId,
                UnitPrice = request.UnitPrice,
                ProductName = request.ProductName
            };

            _unitOfWork.Products.Insert(product); 
            _unitOfWork.Complete(); 
            return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product); 
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _unitOfWork.Products.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            _unitOfWork.Products.Delete(product); 
            _unitOfWork.Complete(); 
            return NoContent(); 
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = _unitOfWork.Products.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product); 
        }

        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, ProductUpdateRequest request)
        {
            var productInDb = _unitOfWork.Products.GetById(id);
            if (productInDb == null)
            {
                return NotFound();
            }

            productInDb.ProductName = request.ProductName;
            productInDb.CategoryId = request.CategoryId;
            productInDb.UnitPrice = request.UnitPrice;
            productInDb.UnitsInStock = request.UnitsInStock;

            _unitOfWork.Products.Update(productInDb); 
            _unitOfWork.Complete(); 
            return NoContent(); 
        }
    }
}
