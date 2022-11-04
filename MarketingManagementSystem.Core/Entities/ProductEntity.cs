using MarketingManagementSystem.Core.Primitives;
using System.ComponentModel.DataAnnotations;

namespace MarketingManagementSystem.Core.Entities;

public sealed class ProductEntity : AggregateRoot
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
