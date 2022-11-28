using MarketingManagementSystem.Application.Models;
using MarketingManagementSystem.Domain.Entities;

namespace MarketingManagementSystem.Application.ResponseModels;

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
public class DistributorBonus
{
    public DistributorBonus(
        Distributor distributor,
        List<Bonus> bonuses)
    {
        Distributor = distributor;
        Bonuses = bonuses;
    }

    public Distributor Distributor { get; set; }
    public List<Bonus> Bonuses { get; set; }
}