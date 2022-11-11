using MarketingManagementSystem.Core.Entities;

namespace MarketingManagementSystem.Core.ResponseModels;

public class DistributorCountedBonus
{
    public DistributorCountedBonus(
        Distributor distributor,
        Bonus bonus)
    {
        Distributor = distributor;
        Bonus = bonus;
    }

    public Distributor Distributor { get; set; }
    public Bonus Bonus { get; set; }
}
