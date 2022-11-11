using AutoMapper;
using MarketingManagementSystem.Core.Entities;
using MarketingManagementSystem.Core.Enums;
using MarketingManagementSystem.Core.Interfaces;
using MediatR;

namespace MarketingManagementSystem.Core.Features.Distributors.Commands;
public sealed record UpdateDistributorCommand : IRequest<bool>
{
    public int Id { get; set; }
    public UpdateDistributorInfo DistributorInfo { get; set; }
    public UpdateDistributorCommand(int id, UpdateDistributorInfo distributorInfo)
    {
        Id = id;
        DistributorInfo = distributorInfo;
    }

    public class UpdateDistributorCommandHandler : IRequestHandler<UpdateDistributorCommand, bool>
    {
        private readonly IDistributorsRepository _distributorsRepository;
        private readonly IMapper _mapper;
        public UpdateDistributorCommandHandler(IDistributorsRepository distributorsRepository, IMapper mapper)
        {
            _distributorsRepository = distributorsRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdateDistributorCommand request, CancellationToken cancellationToken)
        {
            var distributorEntity = await _distributorsRepository.GetDistributorById(request.Id);
            if (distributorEntity == null) throw new Exception("Distributor not found");

            var distributorInfoEntities = await _distributorsRepository.GetDistributorInfoById(distributorEntity.Id);

            var distributorInfoEntity = distributorInfoEntities.DistributorInfo;
            var distributorIdentityCardInfoEntity = distributorInfoEntities.IdentityCardInfo;
            var distributorContactInfoEntity = distributorInfoEntities.ContactInfo;
            var distributorAddressInfoEntity = distributorInfoEntities.AddressInfo;

            var updateDistributorInfo = await UpdateDistributorInfo(distributorInfoEntity, request.DistributorInfo.UpdateDistributor);
            var updateIdentityCardInfo = await UpdateOrCreateIdentityCardInfo(request.Id, distributorIdentityCardInfoEntity, request.DistributorInfo.UpdateIdentityCardInfo);
            var updateDistributorContactInfo = await UpdateOrCreateContactInfo(request.Id, distributorContactInfoEntity, request.DistributorInfo.UpdateContactInfo);
            var updateAddressInfo = await UpdateOrCreateAddressInfo(request.Id, distributorAddressInfoEntity, request.DistributorInfo.UpdateAddressInfo);


            var result = await _distributorsRepository.UpdateDistributorInfo(updateDistributorInfo, updateIdentityCardInfo, updateDistributorContactInfo, updateAddressInfo);

            return result;
        }
        private async Task<DistributorEntity> UpdateDistributorInfo(DistributorEntity entity, UpdateDistributor newInfo)
        {
            if (entity != null && newInfo != null)
            {
                entity.FirstName = newInfo.FirstName ?? entity.FirstName;
                entity.LastName = newInfo.LastName ?? entity.LastName;
                entity.BirthDate = (DateTime)(newInfo.BirthDate != default ? newInfo.BirthDate : entity.BirthDate);
                entity.Gender = (Gender)(newInfo.Gender != default ? newInfo.Gender : entity.Gender);
                entity.Img = newInfo.Img ?? entity.Img;
            }
            return entity;
        }
        private async Task<IdentityCardInfoEntity> UpdateOrCreateIdentityCardInfo(int distributorId, IdentityCardInfoEntity entity, UpdateIdentityCardInfo newInfo)
        {
            if (entity == null && newInfo != null)
            {
                try
                {
                    entity = new IdentityCardInfoEntity(
                        (DocumentType)newInfo.DocumentType,
                        newInfo.DocumentNumber,
                        newInfo.DocumentNumber,
                        (DateTime)newInfo.ReleaseDate,
                        newInfo.TermOfDocument,
                        newInfo.PersonalNumber,
                        newInfo.IssueAgency,
                        distributorId);
                }
                catch (Exception)
                {
                    throw new Exception("Information for update does not found... Fill Required fields!");
                }
                return entity;
            }
            else if (entity != null && newInfo != null)
            {
                entity.DocumentType = (DocumentType)(newInfo.DocumentType == default ? entity.DocumentType : newInfo.DocumentType);
                entity.DocumentSerialNumber = newInfo.DocumentSerialNumber ?? entity.DocumentSerialNumber;
                entity.DocumentNumber = newInfo.DocumentNumber ?? entity.DocumentNumber;
                entity.ReleaseDate = (DateTime)(newInfo.ReleaseDate == default ? entity.ReleaseDate : newInfo.ReleaseDate);
                entity.TermOfDocument = newInfo.TermOfDocument ?? entity.TermOfDocument;
                entity.PersonalNumber = newInfo.PersonalNumber ?? entity.PersonalNumber;
                entity.IssueAgency = newInfo.IssueAgency ?? entity.IssueAgency;
            }

            return entity;
        }
        private async Task<ContactInfoEntity> UpdateOrCreateContactInfo(int distributorId, ContactInfoEntity entity, UpdateContactInfo newInfo)
        {
            if (entity == null && newInfo != null)
            {
                try
                {
                    entity = new ContactInfoEntity(
                        (ContactType)newInfo.ContactType,
                        newInfo.Contact,
                        distributorId);
                }
                catch (Exception)
                {
                    throw new Exception("Information for update does not found... Fill Required fields!");
                }
                return entity;
            }
            else if (entity != null && newInfo != null)
            {
                entity.ContactType = (ContactType)(newInfo.ContactType == default ? entity.ContactType : newInfo.ContactType);
                entity.Contact = newInfo.Contact ?? entity.Contact;
            }
            return entity;
        }
        private async Task<AddressInfoEntity> UpdateOrCreateAddressInfo(int distributorId, AddressInfoEntity entity, UpdateAddressInfo newInfo)
        {
            if (entity == null && newInfo != null)
            {
                try
                {
                    entity = new AddressInfoEntity(
                        (AddressType)newInfo.AddressType,
                        newInfo.Address,
                        distributorId);
                }
                catch (Exception)
                {
                    throw new Exception("Information for update does not found... Fill Required fields!");
                }
                return entity;
            }
            else if (entity != null && newInfo != null)
            {
                entity.AddressType = (AddressType)(newInfo.AddressType == default ? entity.AddressType : newInfo.AddressType);
                entity.Address = newInfo.Address ?? entity.Address;
            }
            return entity;
        }

    }
};

