using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyShopApi.App;
using MyShopApi.App.DTOs.ProductDTOs;
using MyShopApi.Models;

namespace MyShopApi.Repositories
{
    public class ProductRepo : IProductRepo
    {
        private AppDbContext _db;
        private readonly IMapper _mapper;
        public ProductRepo(AppDbContext context,IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
        }

        public IEnumerable<ProductSearchOutput> GetAll(int page)
        {
            try
            {

                var products = _db.Products
                    .Include(i => i.Category)
                    .OrderByDescending(x => x.CreatedAt)
                    .Skip((page - 1) * 10)
                    .Take(10);

                return _mapper.Map<IEnumerable<ProductSearchOutput>>(products);
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        public Product GetById(int id)
        {
            try
            {
                var product = _db.Products.FirstOrDefault(x => x.ID == id);
                return product ?? default;
            }
            catch (Exception)
            {
                return default;
            }
        }
        public int CreateProduct(Product product)
        {
            
                var newProduct = _db.Products.Add(product);
                _db.SaveChanges();
                return newProduct.Entity.ID;
            
           
        }
        public bool UpdateProduct(int id, Product product)
        {
            try
            {
                if (_db.Products.Find(id) is Product current)
                {
                    _db.Entry(current).CurrentValues.SetValues(product);
                }

                // ↓ Property Mapping ↓

                // var current = _db.Products.FirstOrDefault(x => x.ID == id);
                // if (current == null)
                //     return false;

                // current.Title = product.Title;
                // current.RealPrice = product.RealPrice;
                // current.SalesPrice = product.SalesPrice;
                // current.Qty = product.Qty;
                // current.IsPublished = product.IsPublished;

                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteProduct(int id)
        {
            try
            {
                var current = _db.Products.FirstOrDefault(x => x.ID == id);
                if (current == null)
                    return false;
                _db.Products.Remove(current);
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
