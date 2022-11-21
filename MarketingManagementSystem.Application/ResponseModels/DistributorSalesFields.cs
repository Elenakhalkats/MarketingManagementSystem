namespace MarketingManagementSystem.Application.ResponseModels;

public class DistributorSalesFields
{
    public DistributorSalesFields(
        int distributorId,
        float countedTotal)
    {
        DistributorId = distributorId;
        CountedTotal = countedTotal;
    }
    public int DistributorId { get; set; }
    public float CountedTotal { get; set; }
}
