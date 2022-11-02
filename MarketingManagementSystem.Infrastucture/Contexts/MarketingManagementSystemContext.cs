using MarketingManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketingManagementSystem.Infrastucture.Contexts;


public class MarketingManagementSystemContext : DbContext
{
    public MarketingManagementSystemContext(DbContextOptions options)
        : base(options)
    {

    }
    public DbSet<DistributorEntity> Distributors { get; set; }
    public DbSet<IdentityCardInfoEntity> IdentityCardInfos { get; set; }
    public DbSet<ContactInfoEntity> ContactInfos { get; set; }
    public DbSet<AddressInfoEntity> AddressInfos { get; set; }
    public DbSet<DistributorRecommendationEntity> Recommendations { get; set; }
    public DbSet<BonusEntity> DistributorBonuses { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<SaleEntity> Sales { get; set; }

}

