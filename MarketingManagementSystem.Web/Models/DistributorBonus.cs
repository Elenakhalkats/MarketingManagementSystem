namespace MarketingManagementSystem.Web.Models;

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
