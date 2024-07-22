using MyShopApi.App.DTOs.ProductDTOs;
using MyShopApi.Models;

namespace MyShopApi.Repositories
{
    public interface IProductRepo
    {
        IEnumerable<ProductSearchOutput> GetAll(int page);
        Product GetById(int id);
        int CreateProduct(Product product);
        bool UpdateProduct(int id, Product product);
        bool DeleteProduct(int id);

    }
}
