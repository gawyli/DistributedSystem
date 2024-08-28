namespace DistributedSystem.Client.Web.Dtos;

public class ProductDto
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string? SaleOfferId { get; set; }

}
