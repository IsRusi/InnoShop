namespace InnoShop.ProductManagment.Application.DTOs
{
    public class SearchProductsRequest
    {
        public string? SearchTerm { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public bool? IsAvailable { get; set; }
        public Guid? UserId { get; set; }
    }
}
