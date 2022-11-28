namespace MarketingManagementSystem.Application.Models;
public class Sale
{
    public Sale(
        int? id,
        int distributorId,
        DateTime date,
        int productId,
        float unitPrice,
        float totalPrice,
        bool counted)
    {
        Id = id;
        DistributorId = distributorId;
        Date = date;
        ProductId = productId;
        UnitPrice = unitPrice;
        TotalPrice = totalPrice;
        Counted = counted;
    }

    public int? Id { get; set; }
    public int? DistributorId { get; set; }
    public DateTime Date { get; set; }
    public int? ProductId { get; set; }
    public float? UnitPrice { get; set; }
    public float TotalPrice { get; set; }
    public bool Counted { get; set; }
}
