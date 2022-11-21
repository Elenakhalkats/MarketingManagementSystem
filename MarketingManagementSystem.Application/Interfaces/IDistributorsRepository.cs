using MarketingManagementSystem.Application.ResponseModels;
using MarketingManagementSystem.Domain.Entities;

namespace MarketingManagementSystem.Application.Interfaces;

public interface IDistributorsRepository
{
    Task<List<DistributorEntity>> GetDistributorsAsync(DistributorsFilterObjects? distributorsFilterObjects);
    Task<DistributorEntity> GetDistributorByIdAsync(int Id);
    Task<DistributorInfoEntities> GetDistributorInfoByIdAsync(int Id);
    Task<DistributorEntity> AddDistributorAsync(DistributorEntity Distributor);
    Task<int> AddDistributorInfoAsync(DistributorInfoEntities Distributor, int DistributorId);
    Task<bool> UpdateDistributorInfoAsync(DistributorEntity distributor,
                                    IdentityCardInfoEntity identityCardInfo,
                                    ContactInfoEntity contactInfo,
                                    AddressInfoEntity addressInfo);
    Task<bool> DeleteDistributorAsync(int Id);
    Task<List<DistributorEntity>> GetRecommendationsByIdAsync(int Id);
    Task<bool> RecommendDistributorAsync(int RecommendatorId, int RecommendToId);
    Task<DistributorBonuses> GetBonusesByDistributorIdAsync(int Id);
    Task<List<BonusEntity>> GetBonusesAsync();
    Task<DistributorEntity> GetMinBonusAsync();
    Task<DistributorEntity> GetMaxBonusAsync();
    Task<List<RecommendationEntity>> GetRecommendationsAsync();
    Task<bool> AddBonusesAsync(List<BonusEntity> bonuses);
}
