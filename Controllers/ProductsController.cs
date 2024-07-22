using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyShopApi.App.DTOs.ProductDTOs;
using MyShopApi.Models;
using MyShopApi.Repositories;

namespace MyShopApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductRepo _repo;
        public ProductsController(IProductRepo repository)
        {
            _repo = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductSearchOutput>> Search(int page) 
            => Ok(_repo.GetAll(page));

        [HttpGet("{id}")]
        public ActionResult<Product> Read(int id)
        {
            var product = _repo.GetById(id);
            if(product is not null)
                return Ok(product);
            return NotFound("Product not found!");
        }

        [HttpPost]
        public ActionResult Create(Product input)
        {
            var productID = _repo.CreateProduct(input);
            if(productID > 0)
                return Ok(productID);
            return BadRequest("Create product failed!");
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute]int id, [FromBody]Product input)
        {
            var result = _repo.UpdateProduct(id,input);
            if (result)
                return Ok();
            return BadRequest("Update product failed!");
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var result = _repo.DeleteProduct(id);
            if (result)
                return Ok();
            return BadRequest("Delete product failed!");
        }

    }
}
