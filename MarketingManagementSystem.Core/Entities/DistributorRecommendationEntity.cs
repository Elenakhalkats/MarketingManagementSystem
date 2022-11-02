using MarketingManagementSystem.Core.Primitives;
using System.ComponentModel.DataAnnotations;

namespace MarketingManagementSystem.Core.Entities;

public sealed class DistributorRecommendationEntity : AggregateRoot
{
    public DistributorRecommendationEntity(
        int recommendator,
        int recommendTo,
        int hierarchy)
    {
        Recommendator = recommendator;
        RecommendTo = recommendTo;
        Hierarchy = hierarchy;
    }

    [Required]
    public int Recommendator { get; set; }
    [Required]
    public int RecommendTo { get; set; }
    [Required]
    public int Hierarchy { get; set; }
}
