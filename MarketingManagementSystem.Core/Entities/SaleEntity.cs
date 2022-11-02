using MarketingManagementSystem.Core.Primitives;
using System.ComponentModel.DataAnnotations;

namespace MarketingManagementSystem.Core.Entities;

public class SaleEntity : AggregateRoot
{
    public SaleEntity(
        int distributorId,
        DateTime date,
        int productId,
        float? unitPrice,
        float totalPrice,
        bool counted = false)
    {
        DistributorId = distributorId;
        Date = date;
        ProductId = productId;
        UnitPrice = unitPrice;
        TotalPrice = totalPrice;
        Counted = counted;
    }
    [Required]
    public int DistributorId { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public int ProductId { get; set; }
    public float? UnitPrice { get; set; }
    [Required]
    public float TotalPrice { get; set; }
    [Required]
    public bool Counted { get; set; }
}
