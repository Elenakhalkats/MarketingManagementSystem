namespace MarketingManagementSystem.Application.Models;
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
