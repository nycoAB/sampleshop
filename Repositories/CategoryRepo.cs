using Microsoft.EntityFrameworkCore;
using MyShopApi.App;
using MyShopApi.Models;

namespace MyShopApi.Repositories
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly AppDbContext _context;
        public CategoryRepo(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Category> GetCategories() { 
            var categories = _context.Categories.Include(p => p.Products).ToList();
            return categories;
            
        }
        public void CreateCategory(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
            }
            catch (Exception) { 
                            
            }
        }

        public void DeleteCategory(int id)
        {
            try
            {

                var category = _context.Categories.FirstOrDefault(c => c.ID == id);
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
            catch (Exception) { }
        }

        public void UpdateCategory(int id , Category category)
        {
            try
            {
                if (category.ID == id)
                {
                    _context.Categories.Update(category);
                    _context.SaveChanges();
                }
            }
            catch (Exception) { }   

        }
    }
}
