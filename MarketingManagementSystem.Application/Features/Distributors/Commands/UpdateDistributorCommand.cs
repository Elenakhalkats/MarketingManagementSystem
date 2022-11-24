using AutoMapper;
using MarketingManagementSystem.Application.Exceptions;
using MarketingManagementSystem.Application.Interfaces;
using MarketingManagementSystem.Domain.Entities;
using MarketingManagementSystem.Domain.Enums;
using MediatR;

namespace MarketingManagementSystem.Application.Features.Distributors.Commands;
public sealed record UpdateDistributorCommand : IRequest<int>
{
    public int Id { get; set; }
    public UpdateDistributorInfo DistributorInfo { get; set; }
    public UpdateDistributorCommand(int id, UpdateDistributorInfo distributorInfo)
    {
        Id = id;
        DistributorInfo = distributorInfo;
    }

    public class UpdateDistributorCommandHandler : IRequestHandler<UpdateDistributorCommand, int>
    {
        private readonly IDistributorsRepository _distributorsRepository;
        private readonly IMapper _mapper;
        public UpdateDistributorCommandHandler(IDistributorsRepository distributorsRepository, IMapper mapper)
        {
            _distributorsRepository = distributorsRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateDistributorCommand request, CancellationToken cancellationToken)
        {
            var distributorEntity = await _distributorsRepository.GetDistributorByIdAsync(request.Id);
            if (distributorEntity == null) throw new DistributorNotFoundException();

            var distributorInfos = await _distributorsRepository.GetDistributorInfoByIdAsync(distributorEntity.Id);
            
            var updateDistributorInfo = request.DistributorInfo.UpdateDistributor != null ? await UpdateDistributorInfo(distributorInfos.DistributorInfo, request.DistributorInfo.UpdateDistributor) : null;
            var updateIdentityCardInfo = request.DistributorInfo.UpdateIdentityCardInfo != null ? await UpdateOrCreateIdentityCardInfo(request.Id, distributorInfos.IdentityCardInfo, request.DistributorInfo.UpdateIdentityCardInfo) : null;
            var updateDistributorContactInfo = request.DistributorInfo.UpdateContactInfo != null ? await UpdateOrCreateContactInfo(request.Id, distributorInfos.ContactInfo, request.DistributorInfo.UpdateContactInfo) : null;
            var updateAddressInfo = request.DistributorInfo.UpdateAddressInfo != null ? await UpdateOrCreateAddressInfo(request.Id, distributorInfos.AddressInfo, request.DistributorInfo.UpdateAddressInfo) : null;

            var result = await _distributorsRepository.UpdateDistributorInfoAsync(updateDistributorInfo, updateIdentityCardInfo, updateDistributorContactInfo, updateAddressInfo);
            return result;
        }
        private async Task<DistributorEntity> UpdateDistributorInfo(DistributorEntity entity, UpdateDistributor newInfo)
        {
            if (entity != null)
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
            if (entity == null) return new IdentityCardInfoEntity(
                        (DocumentType)newInfo.DocumentType,
                        newInfo.DocumentNumber,
                        newInfo.DocumentNumber,
                        (DateTime)newInfo.ReleaseDate,
                        newInfo.TermOfDocument,
                        newInfo.PersonalNumber,
                        newInfo.IssueAgency,
                        distributorId);

            if (entity != null)
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
            if (entity == null) return new ContactInfoEntity(
                        (ContactType)newInfo.ContactType,
                        newInfo.Contact,
                        distributorId);
            if (entity != null)
            {
                entity.ContactType = (ContactType)(newInfo.ContactType == default ? entity.ContactType : newInfo.ContactType);
                entity.Contact = newInfo.Contact ?? entity.Contact;
            }
            return entity;
        }
        private async Task<AddressInfoEntity> UpdateOrCreateAddressInfo(int distributorId, AddressInfoEntity entity, UpdateAddressInfo newInfo)
        {
            if (entity == null) return new AddressInfoEntity(
                        (AddressType)newInfo.AddressType,
                        newInfo.Address,
                        distributorId);
            if (entity != null)
            {
                entity.AddressType = (AddressType)(newInfo.AddressType == default ? entity.AddressType : newInfo.AddressType);
                entity.Address = newInfo.Address ?? entity.Address;
            }
            return entity;
        }
    }
};

