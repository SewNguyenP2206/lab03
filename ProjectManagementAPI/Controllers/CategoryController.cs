using BussinessObjects.DTOs;
using BussinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using UnitOfWorks.Interfaces;

namespace ProjectManagementAPI.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategories()
        {
            var categories = _unitOfWork.Categories.GetAll(); 
            return Ok(categories);
        }

        // POST: api/categories
        [HttpPost]
        public IActionResult PostCategory(CategoryCreateRequest request)
        {
            Category category = new Category
            {
                CategoryName = request.CategoryName,
            };

            _unitOfWork.Categories.Insert(category); 
            _unitOfWork.Complete(); 
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.CategoryId }, category);
        }

        // GET: api/categories/{id}
        [HttpGet("{id}")]
        public ActionResult<Category> GetCategoryById(int id)
        {
            var category = _unitOfWork.Categories.GetById(id);
            if (category == null)
            {
                return NotFound(); 
            }

            return Ok(category);
        }

        // DELETE: api/categories/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _unitOfWork.Categories.GetById(id);
            if (category == null)
            {
                return NotFound(); 
            }

            _unitOfWork.Categories.Delete(category); 
            _unitOfWork.Complete(); 
            return NoContent(); 
        }

        // PUT: api/categories/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCategory(int id, CategoryUpdateRequest request)
        {
            var categoryInDb = _unitOfWork.Categories.GetById(id);
            if (categoryInDb == null)
            {
                return NotFound(); 
            }

            categoryInDb.CategoryName = request.CategoryName; 

            _unitOfWork.Categories.Update(categoryInDb); 
            _unitOfWork.Complete();
            return NoContent(); 
        }
    }
}
