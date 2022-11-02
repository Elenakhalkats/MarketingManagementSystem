using MarketingManagementSystem.Core.Entities;

namespace MarketingManagementSystem.Core.Models;

public class DistributorBonuses
{
    public DistributorBonuses(
        DistributorEntity distributorEntity,
        List<BonusEntity> bonusEntities)
    {
        Distributor = distributorEntity;
        BonusEntities = bonusEntities;
    }
    public DistributorEntity Distributor { get; set; }
    public List<BonusEntity> BonusEntities { get; set; }
}
