using MyShopApi.Models;

namespace MyShopApi.Repositories
{
    public interface ICategoryRepo
    {   
        public IEnumerable<Category> GetCategories();
        public void CreateCategory(Category category);
        public void UpdateCategory(int id ,Category category);
        public void DeleteCategory(int id);

    }
}
