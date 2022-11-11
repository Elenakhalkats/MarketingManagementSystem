using MarketingManagementSystem.Core.Primitives;
using System.ComponentModel.DataAnnotations;

namespace MarketingManagementSystem.Core.Entities;

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
    [Required]
    public string ProductCode { get; set; }
    [Required]
    public string ProductName { get; set; }
    [Required]
    public float UnitPrice { get; set; }
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