using Microsoft.AspNetCore.Mvc;
using MyShopApi.App.DTOs.ProductDTOs;
using MyShopApi.Models;
using MyShopApi.Repositories;

namespace MyShopApi.Controllers
{
    [Route("/api/products")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly ProductRepo _repo;

        public ProductsController(ProductRepo repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public ActionResult<IEnumerable<ProductSearchOutput>> Search(int page)
            => Ok(_repo.GetAll(page));

        [HttpGet("{id}")]
        public ActionResult<Product> Read(int id)
        {
            var product = _repo.GetById(id);
            if (product is not null)
                return Ok(product);
            return NotFound("Product not found!");
        }

    }
}
