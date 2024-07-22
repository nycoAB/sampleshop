namespace MyShopApi.Models
{
    public class User
    {
        public User()
        {
            Products = new HashSet<Product>();
        }
        public int ID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

        public IEnumerable<Product> Products { get; set; }

    }
}
