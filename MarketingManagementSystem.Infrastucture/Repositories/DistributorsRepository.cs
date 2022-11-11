﻿using MarketingManagementSystem.Core.Entities;
using MarketingManagementSystem.Core.Interfaces;
using MarketingManagementSystem.Core.ResponseModels;
using MarketingManagementSystem.Infrastucture.Contexts;
using Microsoft.EntityFrameworkCore;

namespace MarketingManagementSystem.Infrastucture.Repositories;

public class DistributorsRepository : IDistributorsRepository
{
    private readonly MarketingManagementSystemContext _context;

    public DistributorsRepository(MarketingManagementSystemContext context)
    {
        _context = context;
    }  

    public async Task<List<DistributorEntity>> GetDistributors(DistributorsFilterObjects? distributorsFilterObjects)
    {
        var distributors = _context.Distributors.ToList();

        if(distributorsFilterObjects != null)
        {
            if (distributorsFilterObjects.FirstName != null) distributors = (List<DistributorEntity>)distributors.FindAll(x => x.FirstName == distributorsFilterObjects.FirstName);
            if (distributorsFilterObjects.LastName != null) distributors = (List<DistributorEntity>)distributors.FindAll(x => x.LastName == distributorsFilterObjects.LastName);
        }
        return distributors.ToList();
    }
    public async Task<DistributorEntity> GetDistributorById(int Id)
    {
        var distributor = _context.Distributors.FirstOrDefault(x => x.Id == Id);
        if (distributor == null) throw new Exception("Distributor Not Found!");
        return distributor;
    }
    public async Task<DistributorInfoEntities> GetDistributorInfoById(int Id)
    {
        var Distributor = await GetDistributorById(Id);

        var IdentityCardInfo = _context.IdentityCardInfos.FirstOrDefault(x => x.DistributorId == Id);
        var ContactInfo = _context.ContactInfos.FirstOrDefault(x => x.DistributorId == Id);
        var AddressInfo = _context.AddressInfos.FirstOrDefault(x => x.DistributorId == Id);

        var distributorDetails = new DistributorInfoEntities(Distributor, IdentityCardInfo, ContactInfo, AddressInfo);
        return distributorDetails;
    }
    public async Task<DistributorEntity> AddDistributor(DistributorEntity Distributor)
    {
        var distributor = _context.Distributors.AddAsync(Distributor).Result;
        _context.SaveChanges();
        
        return distributor.Entity;
    }
    public async Task<int> AddDistributorInfo(DistributorInfoEntities Distributor, int DistributorId)
    {
        if(Distributor.ContactInfo != null) _context.ContactInfos.Add(Distributor.ContactInfo); 
        if(Distributor.IdentityCardInfo != null) _context.IdentityCardInfos.Add(Distributor.IdentityCardInfo); 
        if(Distributor.AddressInfo != null) _context.AddressInfos.Add(Distributor.AddressInfo);

        _context.SaveChanges();
        return DistributorId;
    }
    public async Task<bool> UpdateDistributorInfo(DistributorEntity? distributor,
                                                    IdentityCardInfoEntity? identityCardInfo,
                                                    ContactInfoEntity? contactInfo,
                                                    AddressInfoEntity? addressInfo)
    {

        if (distributor != null) await UpdateDistributorInfo(distributor);
        if (identityCardInfo != null) await UpdateOrCreateIdentityCardInfo(identityCardInfo);
        if (contactInfo != null) await UpdateOrCreateContactInfo(contactInfo);
        if (addressInfo != null) await UpdateOrCreateAddressInfo(addressInfo);

        return true;
    }
    public async Task<bool> DeleteDistributor(int Id)
    {
        var distributorEntity = await GetDistributorById(Id);
        _context.Distributors.Remove(distributorEntity);
        
        var identityCardInfoEntity = _context.IdentityCardInfos.FirstOrDefault(x => x.DistributorId == Id);
        if(identityCardInfoEntity != null) _context.IdentityCardInfos.Remove(identityCardInfoEntity);

        var contactInfoEntity = _context.ContactInfos.FirstOrDefault(x => x.DistributorId == Id);
        if(contactInfoEntity != null) _context.ContactInfos.Remove(contactInfoEntity);

        var addressInfoEntity = _context.AddressInfos.FirstOrDefault(x => x.DistributorId == Id);
        if(addressInfoEntity != null) _context.AddressInfos.Remove(addressInfoEntity);

        var saleEntities = _context.Sales.Where(x => x.DistributorId == Id);
        if (saleEntities.Count() != 0) _context.Sales.RemoveRange(saleEntities);

        _context.SaveChanges();
        return true;
    }
    public async Task<List<DistributorEntity>> GetRecommendationsById(int Id)
    {
        var Distributor = await GetDistributorById(Id);
        
        var distributors = new List<DistributorEntity>();

        var recoms = _context.Recommendations.ToList().FindAll(x => x.Recommendator == Id);
        foreach (var recommendation in recoms)
        {
            var distributor = _context.Distributors.ToList().FirstOrDefault(x => x.Id == recommendation.RecommendTo);
            if(distributor != null) distributors.Add(distributor);
        }

        return distributors;
    }

    public async Task<bool> RecommendDistributor(int RecommendatorId, int RecommendToId)
    {
        var recommendToDist = await GetDistributorById(RecommendToId); 

        var recommendTo = _context.Recommendations.FirstOrDefault(x => x.RecommendTo == RecommendToId);
        if (recommendTo != null) throw new Exception("This distributor already has a recommendator!");

        var distributorAsRecommendator = await GetDistributorById(RecommendatorId);
        if (!distributorAsRecommendator.RecommendAccess) throw new Exception("This recommendator can't have more recommendations");
        
        var hierarchyForRecommendation = 0;

        var distributorAsRecommendTo = _context.Recommendations.FirstOrDefault(x => x.RecommendTo == RecommendatorId);
        if (distributorAsRecommendTo != null)
        {
            var hierarchy = distributorAsRecommendTo.Hierarchy;
            if (hierarchy >= 4) throw new Exception("This recommendator is not acceptable");
            hierarchyForRecommendation = hierarchy + 1;
        }
        else
        {
            hierarchyForRecommendation = 1;
        }

        var recommedations = _context.Recommendations.ToList();

        recommedations = (List<DistributorRecommendationEntity>)recommedations.FindAll(x => x.Recommendator == RecommendatorId);
        var count = recommedations.Count();

        if (count == 2)
        {
            distributorAsRecommendator.RecommendAccess = false;
            _context.Distributors.Update(distributorAsRecommendator);
            _context.SaveChanges();
        }

        var recommendation = new DistributorRecommendationEntity(
            RecommendatorId,
            RecommendToId,
            hierarchyForRecommendation);

        _context.Recommendations.Add(recommendation);
        _context.SaveChanges();

        return true;
    }

    public async Task<DistributorBonuses> GetBonusesByDistributorId(int Id)
    {
        var bonuses = _context.DistributorBonuses.Where(x => x.DistributorId == Id).ToList();
        var distributor = await GetDistributorById(Id);

        var result = new DistributorBonuses(
            distributor,
            bonuses);
        
        return result;
    }
    public async Task<List<DistributorRecommendationEntity>> GetRecommendations()
    {
        var recommendations = _context.Recommendations.ToList();
        return recommendations;
    }
    public async Task<bool> AddBonuses(List<BonusEntity> bonuses)
    {
        _context.DistributorBonuses.AddRange(bonuses);
        _context.SaveChanges();

        return true;
    }

    public async Task<DistributorEntity> GetDistributorWithMinBonus()
    {
        var bonuses = _context.DistributorBonuses.Select(x => new { x.DistributorId, x.CountedBonus });
        var minBonus = bonuses.Select(x => x.CountedBonus).ToList().Min();
        var minBonusEntity = bonuses.FirstOrDefault(x => x.CountedBonus == minBonus);

        var distributorId = minBonusEntity.DistributorId;
        var distributor = await GetDistributorById(distributorId);

        return distributor;
    }

    public async Task<DistributorEntity> GetDistributorWithMaxBonus()
    {
        var bonuses = _context.DistributorBonuses.Select(x => new { x.DistributorId, x.CountedBonus });
        var maxBonus = bonuses.Select(x => x.CountedBonus).ToList().Max();
        var maxBonusEntity = bonuses.FirstOrDefault(x => x.CountedBonus == maxBonus);

        var distributorId = maxBonusEntity.DistributorId;
        var distributor = await GetDistributorById(distributorId);

        return distributor;
    }
    private async Task<bool> UpdateDistributorInfo(DistributorEntity entity)
    {
        var distributorInfo = await GetDistributorById(entity.Id);

        distributorInfo.FirstName = entity.FirstName;
        distributorInfo.LastName = entity.LastName;
        distributorInfo.BirthDate = entity.BirthDate;
        distributorInfo.Gender = entity.Gender;
        distributorInfo.Img = entity.Img;

        _context.Update(distributorInfo);
        await _context.SaveChangesAsync();
        return true;
    }
    private async Task<bool> UpdateOrCreateIdentityCardInfo(IdentityCardInfoEntity entity)
    {
        var identityCardInfo = await _context.IdentityCardInfos.FirstOrDefaultAsync(x => x.Id == entity.Id);

        if (identityCardInfo == null)
        {
            _context.Add(entity);
        }
        else
        {
            identityCardInfo.DocumentType = entity.DocumentType;
            identityCardInfo.DocumentSerialNumber = entity.DocumentSerialNumber;
            identityCardInfo.DocumentNumber = entity.DocumentNumber;
            identityCardInfo.ReleaseDate = entity.ReleaseDate;
            identityCardInfo.TermOfDocument = entity.TermOfDocument;
            identityCardInfo.PersonalNumber = entity.PersonalNumber;
            identityCardInfo.IssueAgency = entity.IssueAgency;

            _context.Update(identityCardInfo);
        }

        await _context.SaveChangesAsync();
        return true;
    }
    private async Task<bool> UpdateOrCreateContactInfo(ContactInfoEntity entity)
    {
        var contactEntity = await _context.ContactInfos.FirstOrDefaultAsync(x => x.Id == entity.Id);

        if (contactEntity == null)
        {
            _context.Add(entity);
        }
        else
        {
            contactEntity.ContactType = entity.ContactType;
            contactEntity.Contact = entity.Contact;

            _context.Update(contactEntity);
        }
        await _context.SaveChangesAsync();
        return true;
    }
    private async Task<bool> UpdateOrCreateAddressInfo(AddressInfoEntity entity)
    {
        var addressEntity = await _context.AddressInfos.FirstOrDefaultAsync(x => x.Id == entity.Id);

        if (addressEntity == null)
        {
            _context.Add(entity);
        }
        else
        {
            addressEntity.AddressType = entity.AddressType;
            addressEntity.Address = entity.Address;

            _context.Update(addressEntity);
        }
        await _context.SaveChangesAsync();
        return true;
    }
}
