namespace MarketingManagementSystem.Core.Models;

public class SalesFilterObjects
{
    public SalesFilterObjects(
        int? distributorId, 
        DateTime? startDate, 
        DateTime? endDate, 
        int? productId,
        bool? counted = default)
    {
        DistributorId = distributorId;
        StartDate = startDate;
        EndDate = endDate;
        ProductId = productId;
        Counted = counted;  
    }   

    public int? DistributorId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? ProductId { get; set; }
    public bool? Counted { get; set; }
}
