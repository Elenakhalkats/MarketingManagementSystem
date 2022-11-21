using MarketingManagementSystem.Domain.Entities;
using MarketingManagementSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketingManagementSystem.Infrastucture.Config;

public class DistributorConfiguration : IEntityTypeConfiguration<DistributorEntity>
{
    public void Configure(EntityTypeBuilder<DistributorEntity> builder)
    {
        builder.Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(p => p.LastName)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(p => p.BirthDate)
            .IsRequired();
        builder.Property(p => p.Gender)
            .IsRequired()
            .HasDefaultValue(Gender.NotSpecified);
        builder.Property(p => p.Img);
        builder.Property(p => p.RecommendAccess)
            .IsRequired()
            .HasDefaultValue(true);
    }
}
public class AddressInfoConfiguration : IEntityTypeConfiguration<AddressInfoEntity>
{
    public void Configure(EntityTypeBuilder<AddressInfoEntity> builder)
    {
        builder.Property(p => p.AddressType)
            .IsRequired();
        builder.Property(p => p.Address)
            .IsRequired()
            .HasMaxLength(100);
        builder.HasOne(x => x.Distributor)
            .WithMany(x => x.AddressInfos)
            .HasForeignKey(x => x.DistributorId);
    }
}
public class ContactInfoConfiguration : IEntityTypeConfiguration<ContactInfoEntity>
{
    public void Configure(EntityTypeBuilder<ContactInfoEntity> builder)
    {
        builder.Property(p => p.ContactType)
            .IsRequired();
        builder.Property(p => p.Contact)
            .IsRequired()
            .HasMaxLength(100);
        builder.HasOne(x => x.Distributor)
            .WithMany(x => x.ContactInfos)
            .HasForeignKey(x => x.DistributorId);
    }
}
public class IdentityCardInfoConfiguration : IEntityTypeConfiguration<IdentityCardInfoEntity>
{
    public void Configure(EntityTypeBuilder<IdentityCardInfoEntity> builder)
    {
        builder.Property(p => p.DocumentType)
            .IsRequired();
        builder.Property(p => p.DocumentSerialNumber)
            .HasMaxLength(10);
        builder.Property(p => p.DocumentNumber)
            .HasMaxLength(10);
        builder.Property(p => p.ReleaseDate)
            .IsRequired();
        builder.Property(p => p.TermOfDocument)
            .IsRequired();
        builder.Property(p => p.PersonalNumber)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(p => p.IssueAgency)
            .HasMaxLength(100);
        builder.HasOne(x => x.Distributor)
            .WithMany(x => x.IdentityCardInfos)
            .HasForeignKey(x => x.DistributorId);
    }
}
public class RecommendationsConfiguration : IEntityTypeConfiguration<RecommendationEntity>
{
    public void Configure(EntityTypeBuilder<RecommendationEntity> builder)
    {
        builder.Property(p => p.Hierarchy)
            .IsRequired();
        builder.Property(p => p.RecommendedId)
            .IsRequired();
        builder.HasOne(x => x.Recommendator)
            .WithMany(x => x.AsRecommendators)
            .HasForeignKey(x => x.RecommendatorId);
    }
}
public class BonusConfiguration : IEntityTypeConfiguration<BonusEntity>
{
    public void Configure(EntityTypeBuilder<BonusEntity> builder)
    {
        builder.Property(p => p.StartDate)
            .IsRequired();
        builder.Property(p => p.EndDate)
            .IsRequired();
        builder.Property(p => p.CountedBonus)
            .IsRequired();
        builder.HasOne(x => x.Distributor)
            .WithMany(x => x.Bonuses)
            .HasForeignKey(x => x.DistributorId);
    }
}
public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.Property(p => p.ProductCode)
            .IsRequired();
        builder.Property(p => p.ProductName)
            .HasMaxLength(10);
        builder.Property(p => p.UnitPrice)
            .IsRequired();
    }
}
public class SaleConfiguration : IEntityTypeConfiguration<SaleEntity>
{
    public void Configure(EntityTypeBuilder<SaleEntity> builder)
    {
        builder.Property(p => p.Date)
            .IsRequired();
        builder.Property(p => p.UnitPrice);
        builder.Property(p => p.TotalPrice)
            .IsRequired();
        builder.Property(p => p.Counted)
            .IsRequired()
            .HasDefaultValue(false);
        builder.HasOne(x => x.Distributor)
            .WithMany(x => x.Sales)
            .HasForeignKey(x => x.DistributorId);
        builder.HasOne(x => x.Product)
            .WithMany(x => x.Sales)
            .HasForeignKey(x => x.ProductId);
    }
}