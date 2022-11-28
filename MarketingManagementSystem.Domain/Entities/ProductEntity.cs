using MarketingManagementSystem.Domain.Primitives;

namespace MarketingManagementSystem.Domain.Entities;

public sealed class ProductEntity : Entity<int>
{
    public ProductEntity(
        string productCode,
        string productName,
        float unitPrice)
    {
        ProductCode = productCode;
        ProductName = productName;  
        UnitPrice = unitPrice;
    }
    public string ProductCode { get; set; }
    public string ProductName { get; set; }
    public float UnitPrice { get; set; }
    public List<SaleEntity> Sales { get; set; }
}
