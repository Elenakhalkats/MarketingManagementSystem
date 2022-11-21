using MarketingManagementSystem.Domain.Primitives;

namespace MarketingManagementSystem.Domain.Entities;

public sealed class RecommendationEntity : Entity<int>
{
    public RecommendationEntity()
    {

    }
    public RecommendationEntity(
        int recommendator,
        int recommendTo,
        int hierarchy)
    {
        RecommendatorId = recommendator;
        RecommendedId = recommendTo;
        Hierarchy = hierarchy;
    }

    public RecommendationEntity(int recommendatorId, int recommendedId, int hierarchy, DistributorEntity recommendator)
    {
        RecommendatorId = recommendatorId;
        RecommendedId = recommendedId;
        Hierarchy = hierarchy;
        Recommendator = recommendator;
    }

    public int RecommendatorId { get; set; }
    public int RecommendedId { get; set; }
    public int Hierarchy { get; set; }
    public DistributorEntity Recommendator { get; set; }

}
