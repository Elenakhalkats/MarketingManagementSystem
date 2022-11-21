using MarketingManagementSystem.Application.Exceptions;
using MarketingManagementSystem.Application.Interfaces;
using MarketingManagementSystem.Application.ResponseModels;
using MarketingManagementSystem.Domain.Entities;
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

    public async Task<List<DistributorEntity>> GetDistributorsAsync(DistributorsFilterObjects? distributorsFilterObjects)
    {
        var distributors = await _context.Distributors.ToListAsync();

        if(distributorsFilterObjects != null)
        {
            var firstNameHasValue = !string.IsNullOrEmpty(distributorsFilterObjects.FirstName);
            var lastNameHasValue = !string.IsNullOrEmpty(distributorsFilterObjects.LastName);

            if (firstNameHasValue) distributors = distributors.FindAll(x => x.FirstName == distributorsFilterObjects.FirstName);
            if (lastNameHasValue) distributors = distributors.FindAll(x => x.LastName == distributorsFilterObjects.LastName);
        }

        return distributors;
    }


    public async Task<DistributorEntity> GetDistributorByIdAsync(int Id)
    {
        var distributor = await _context.Distributors.FirstOrDefaultAsync(x => x.Id == Id);
        return distributor;
    }
    public async Task<DistributorInfoEntities> GetDistributorInfoByIdAsync(int Id)
    {

        var query = from distributor in _context.Distributors.Where(x => x.Id == Id)
                    from identityInfo in _context.IdentityCardInfos.Where(x => x.DistributorId == Id).DefaultIfEmpty()
                    from contactInfo in _context.ContactInfos.Where(x => x.DistributorId == Id).DefaultIfEmpty()
                    from addressInfo in _context.AddressInfos.Where(x => x.DistributorId == Id).DefaultIfEmpty()
                    select new DistributorInfoEntities
                    {
                        DistributorInfo = distributor,
                        IdentityCardInfo = identityInfo,
                        ContactInfo = contactInfo,
                        AddressInfo = addressInfo
                    };

        var distributorInfos = await query.FirstOrDefaultAsync();
        return distributorInfos;
    }
    public async Task<DistributorEntity> AddDistributorAsync(DistributorEntity Distributor)
    {
        var distributor = await _context.Distributors.AddAsync(Distributor);
        _context.SaveChanges();

        return distributor.Entity;
    }
    public async Task<int> AddDistributorInfoAsync(DistributorInfoEntities Distributor, int DistributorId)
    {
        if (Distributor.ContactInfo != null) await _context.ContactInfos.AddAsync(Distributor.ContactInfo);
        if (Distributor.IdentityCardInfo != null) await _context.IdentityCardInfos.AddAsync(Distributor.IdentityCardInfo);
        if (Distributor.AddressInfo != null) await _context.AddressInfos.AddAsync(Distributor.AddressInfo);

        _context.SaveChanges();
        return DistributorId;
    }
    public async Task<bool> UpdateDistributorInfoAsync(DistributorEntity? distributor,
                                                    IdentityCardInfoEntity? identityCardInfo,
                                                    ContactInfoEntity? contactInfo,
                                                    AddressInfoEntity? addressInfo)
    {

        if (distributor != null) await UpdateDistributorInfoAsync(distributor);
        if (identityCardInfo != null) await UpdateOrCreateIdentityCardInfoAsync(identityCardInfo);
        if (contactInfo != null) await UpdateOrCreateContactInfoAsync(contactInfo);
        if (addressInfo != null) await UpdateOrCreateAddressInfoAsync(addressInfo);

        return true;
    }
    public async Task<bool> DeleteDistributorAsync(int Id)
    {
        var distributorEntity = await GetDistributorByIdAsync(Id);
        _context.Distributors.Remove(distributorEntity);

        _context.SaveChanges();
        return true;
    }
    public async Task<List<DistributorEntity>> GetRecommendationsByIdAsync(int Id)
    {
        var distributors = new List<DistributorEntity>();

        var recoms = await _context.Recommendations.Where(x => x.RecommendatorId == Id).ToListAsync();
        foreach (var recommendation in recoms)
        {
            var distributor = await _context.Distributors.FirstOrDefaultAsync(x => x.Id == recommendation.RecommendedId);
            if (distributor != null) distributors.Add(distributor);
        }
        return distributors;
    }

    public async Task<bool> RecommendDistributorAsync(int RecommendatorId, int RecommendToId)
    {
        var recommendToDist = await GetDistributorByIdAsync(RecommendToId);

        var recommendTo = await _context.Recommendations.FirstOrDefaultAsync(x => x.RecommendedId == RecommendToId);
        if (recommendTo != null) throw new AlreadyRecommendedDistributorException();

        var distributorAsRecommendator = await GetDistributorByIdAsync(RecommendatorId);
        if (!distributorAsRecommendator.RecommendAccess) throw new HasNotRecommendAccessException();

        var hierarchyForRecommendation = 0;

        var distributorAsRecommendTo = await _context.Recommendations.FirstOrDefaultAsync(x => x.RecommendedId == RecommendatorId);
        if (distributorAsRecommendTo != null)
        {
            var hierarchy = distributorAsRecommendTo.Hierarchy;
            if (hierarchy >= 4) throw new HasNotRecommendAccessException();
            hierarchyForRecommendation = hierarchy + 1;
        }
        else
        {
            hierarchyForRecommendation = 1;
        }

        var count = await _context.Recommendations.CountAsync(x => x.RecommendatorId == RecommendatorId);

        if (count == 2)
        {
            distributorAsRecommendator.RecommendAccess = false;
            _context.Distributors.Update(distributorAsRecommendator);
            _context.SaveChanges();
        }

        var recommendation = new RecommendationEntity(
            RecommendatorId,
            RecommendToId,
            hierarchyForRecommendation);

        await _context.Recommendations.AddAsync(recommendation);
        _context.SaveChanges();

        return true;
    }

    public async Task<DistributorBonuses> GetBonusesByDistributorIdAsync(int Id)
    {
        var bonuses = _context.DistributorBonuses.Where(x => x.DistributorId == Id).ToList();
        var distributor = await GetDistributorByIdAsync(Id);

        var result = new DistributorBonuses(
            distributor,
            bonuses);

        return result;
    }
    public async Task<List<RecommendationEntity>> GetRecommendationsAsync()
    {
        var recommendations = _context.Recommendations.ToList();
        return recommendations;
    }
    public async Task<bool> AddBonusesAsync(List<BonusEntity> bonuses)
    {
        _context.DistributorBonuses.AddRange(bonuses);
        _context.SaveChanges();

        return true;
    }
    public async Task<List<BonusEntity>> GetBonusesAsync()
    {
        var bonuses = _context.DistributorBonuses.ToList();
        if (bonuses.Count == 0) throw new Exception();
        return bonuses;
    }
    public async Task<DistributorEntity> GetMinBonusAsync()
    {
        var bonuses = _context.DistributorBonuses.Select(x => new { x.DistributorId, x.CountedBonus });
        var minBonus = bonuses.Select(x => x.CountedBonus).ToList().Min();
        var minBonusEntity = bonuses.FirstOrDefault(x => x.CountedBonus == minBonus);

        var distributorId = minBonusEntity.DistributorId;
        var distributor = await GetDistributorByIdAsync(distributorId);

        return distributor;
    }
    public async Task<DistributorEntity> GetMaxBonusAsync()
    {
        var bonuses = _context.DistributorBonuses.Select(x => new { x.DistributorId, x.CountedBonus });
        var minBonus = bonuses.Select(x => x.CountedBonus).ToList().Max();
        var minBonusEntity = bonuses.FirstOrDefault(x => x.CountedBonus == minBonus);

        var distributorId = minBonusEntity.DistributorId;
        var distributor = await GetDistributorByIdAsync(distributorId);

        return distributor;
    }
    private async Task<bool> UpdateDistributorInfoAsync(DistributorEntity entity)
    {
        var distributorInfo = await GetDistributorByIdAsync(entity.Id);

        distributorInfo.FirstName = entity.FirstName;
        distributorInfo.LastName = entity.LastName;
        distributorInfo.BirthDate = entity.BirthDate;
        distributorInfo.Gender = entity.Gender;
        distributorInfo.Img = entity.Img;

        _context.Update(distributorInfo);
        await _context.SaveChangesAsync();
        return true;
    }
    private async Task<bool> UpdateOrCreateIdentityCardInfoAsync(IdentityCardInfoEntity entity)
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
    private async Task<bool> UpdateOrCreateContactInfoAsync(ContactInfoEntity entity)
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
    private async Task<bool> UpdateOrCreateAddressInfoAsync(AddressInfoEntity entity)
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
