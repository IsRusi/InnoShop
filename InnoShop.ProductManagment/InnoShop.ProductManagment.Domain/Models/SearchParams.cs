namespace InnoShop.ProductManagment.Domain.Models
{
    public class SearchParams
{
    public string? SearchTerm { get; set; }
    public double? MinPrice { get; set; }
    public double? MaxPrice { get; set; }
    public bool? IsAvailable { get; set; }
    public Guid? UserId { get; set; }
}
}