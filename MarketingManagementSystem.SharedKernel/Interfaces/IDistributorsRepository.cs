using MarketingManagementSystem.Core.Entities;
using MarketingManagementSystem.Core.Models;

namespace MarketingManagementSystem.SharedKernel.Interfaces;

public interface IDistributorsRepository
{
    Task<List<DistributorEntity>> GetDistributors(DistributorsFilterObjects? distributorsFilterObjects);
    Task<DistributorEntity> GetDistributorById(int Id);
    Task<DistributorInfoEntities> GetDistributorInfoById(int Id);
    Task<DistributorEntity> AddDistributor(DistributorEntity Distributor);
    Task<int> AddDistributorInfo(DistributorInfoEntities Distributor, int DistributorId);
    Task<bool> UpdateDistributorInfo(DistributorEntity distributor,
                                    IdentityCardInfoEntity identityCardInfo,
                                    ContactInfoEntity contactInfo,
                                    AddressInfoEntity addressInfo);
    Task<bool> DeleteDistributor(int Id);
    Task<List<DistributorEntity>> GetRecommendationsById(int Id);
    Task<bool> RecommendDistributor(int RecommendatorId, int RecommendToId);
    Task<DistributorBonuses> GetBonusesByDistributorId(int Id);
    Task<List<BonusEntity>> CountBonus(DateTime StartDate, DateTime EndDate, List<DistributorSalesFields> distributorSale);
    Task<DistributorEntity> GetDistributorWithMinBonus();
    Task<DistributorEntity> GetDistributorWithMaxBonus();
}
