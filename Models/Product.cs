namespace MyShopApi.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int CategoryID { get; set; }
        public int UserID { get; set; }
        public long RealPrice { get; set; }
        public long SalesPrice { get; set; }
        public int Qty { get; set; }
        public bool IsPublished { get; set; }
        public DateTime CreatedAt { get; set; }

        public Category Category { get; set; }
        public User User { get; set; }
    }
}
