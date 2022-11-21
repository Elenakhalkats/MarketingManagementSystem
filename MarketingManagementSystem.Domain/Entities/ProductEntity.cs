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
public class Product
{
    public Product(
       int? id,
       string productCode,
       string productName,
       float unitPrice)
    {
        Id = id;
        ProductCode = productCode;
        ProductName = productName;
        UnitPrice = unitPrice;
    }
    public int? Id { get; set; }
    public string ProductCode { get; set; }
    public string ProductName { get; set; }
    public float UnitPrice { get; set; }
}