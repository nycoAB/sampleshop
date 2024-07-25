using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyShopApi.Models;
using MyShopApi.Repositories;

namespace MyShopApi.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepo _repo;
        public CategoryController(ICategoryRepo repo, IMapper mapper)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAll()
        {
            var categories = _repo.GetCategories();
            return Ok(categories);
        }
    }
}
