using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyShopApi.App.DTOs.ProductDTOs;
using MyShopApi.Models;
using MyShopApi.Repositories;

namespace MyShopApi.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepo _repo;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAll()
        {
            var categories = _repo.GetCategories();
            return Ok(_mapper.Map<IEnumerable<CategoryOutput>>(categories));
        }
    }
}
