using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShopApi.App.DTOs.ProductDTOs;
using MyShopApi.Models;
using MyShopApi.Repositories;

namespace MyShopApi.Controllers._Admin
{
    [Route("/api/admin/category")]
    [ApiController]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepo _repo;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Create(CategoryInput categoryinput)
        {
            var category = _mapper.Map<Category>(categoryinput);
            _repo.CreateCategory(category);
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, CategoryInput categoryinput)
        {
            var category = _mapper.Map<Category>(categoryinput);
            _repo.UpdateCategory(id, category);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repo?.DeleteCategory(id);
            return Ok();
        }
    }
}
