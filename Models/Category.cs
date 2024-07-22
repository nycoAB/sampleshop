namespace MyShopApi.Models
{
    public class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }
        public int ID { get; set; }
        public string Title { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
