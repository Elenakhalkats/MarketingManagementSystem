using MarketingManagementSystem.Application.Interfaces;
using MarketingManagementSystem.Application.Models;
using MarketingManagementSystem.Application.ResponseModels;
using MarketingManagementSystem.Domain.Entities;
using MediatR;


namespace MarketingManagementSystem.Application.Features.Distributors.Commands;

public sealed record AddDistributorCommand(AddDistributorInfo AddDistributorInfo) : IRequest<int>
{
    public class AddDistributorCommandHandler : IRequestHandler<AddDistributorCommand, int>
    {
        private readonly IDistributorsRepository _distributorsRepository;
        public AddDistributorCommandHandler(IDistributorsRepository distributorsRepository)
        {
            _distributorsRepository = distributorsRepository;
        }
        public async Task<int> Handle(AddDistributorCommand request, CancellationToken cancellationToken)
        {
            var addDistributorInfo = request.AddDistributorInfo.Distributor;

            var distributor = new DistributorEntity(
                addDistributorInfo.FirstName,
                addDistributorInfo.LastName,
                addDistributorInfo.BirthDate,
                addDistributorInfo.Gender,
                addDistributorInfo.Img );
            
            var distributorEntity = await _distributorsRepository.AddDistributorAsync(distributor);
            var distributorId = distributorEntity.Id;

            var identityCardInfo = request.AddDistributorInfo.IdentityCardInfo;
            var contactInfo = request.AddDistributorInfo.ContactInfo;
            var addressInfo = request.AddDistributorInfo.AddressInfo;

            var distributorDetails = new DistributorInfoEntities(
                distributorEntity,
                identityCardInfo: identityCardInfo != null ? new IdentityCardInfoEntity(
                    identityCardInfo.DocumentType, 
                    identityCardInfo.DocumentSerialNumber, 
                    identityCardInfo.DocumentNumber,
                    identityCardInfo.ReleaseDate,
                    identityCardInfo.TermOfDocument,
                    identityCardInfo.PersonalNumber,
                    identityCardInfo.IssueAgency,
                    distributorId) : null,
                contactInfo: contactInfo != null ? new ContactInfoEntity(
                    contactInfo.ContactType,
                    contactInfo.Contact,
                    distributorId) : null,
                addressInfo: addressInfo != null ? new AddressInfoEntity(
                    addressInfo.AddressType,
                    addressInfo.Address,
                    distributorId) : null);

            var result = await _distributorsRepository.AddDistributorInfoAsync(distributorDetails, distributorId);
            return result;
        }
    }
};
