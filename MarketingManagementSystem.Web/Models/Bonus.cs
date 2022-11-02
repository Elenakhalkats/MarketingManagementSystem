namespace MarketingManagementSystem.Web.Models;

public class Bonus
{
    public Bonus(
        int? id,
        DateTime startDate, 
        DateTime endDate, 
        float countedBonus)
    {
        Id = id;    
        StartDate = startDate;
        EndDate = endDate;
        CountedBonus = countedBonus;
    }
    public int? Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public float CountedBonus { get; set; }
}
