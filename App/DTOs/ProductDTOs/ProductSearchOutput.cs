namespace MyShopApi.App.DTOs.ProductDTOs
{
    public class ProductSearchOutput
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string CategoryTitle { get; set; }
        public long RealPrice { get; set; }
        public long SalesPrice { get; set; }
        public int Qty { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
