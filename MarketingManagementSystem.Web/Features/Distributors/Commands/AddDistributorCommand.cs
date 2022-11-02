using MarketingManagementSystem.Core.Entities;
using MarketingManagementSystem.Core.Models;
using MarketingManagementSystem.SharedKernel.Interfaces;
using MarketingManagementSystem.Web.Models;
using MediatR;

namespace MarketingManagementSystem.Web.Features.Distributors.Commands;

public sealed record AddDistributorCommand(DistributorInfo DistributorInfo) : IRequest<int>
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
            var distributor = request.DistributorInfo.Distributor;

            try
            {
                var Distributor = distributor != null ? new DistributorEntity(
                distributor.FirstName,
                distributor.LastName,
                distributor.BirthDate,
                distributor.Gender,
                distributor.Img,
                distributor.RecommendAccess) : null;

                var distributorEntity = await _distributorsRepository.AddDistributor(Distributor);

                var identityCardInfo = request.DistributorInfo.IdentityCardInfo;
                var contactInfo = request.DistributorInfo.ContactInfo;
                var addressInfo = request.DistributorInfo.AddressInfo;

                var IdentityCardInfo = identityCardInfo != null ? new IdentityCardInfoEntity(
                    identityCardInfo.DocumentType,
                    identityCardInfo.DocumentSerialNumber,
                    identityCardInfo.DocumentNumber,
                    identityCardInfo.ReleaseDate,
                    identityCardInfo.TermOfDocument,
                    identityCardInfo.PersonalNumber,
                    identityCardInfo.IssueAgency,
                    distributorEntity.Id) : null;

                var ContactInfo = contactInfo != null ? new ContactInfoEntity(
                    contactInfo.ContactType,
                    contactInfo.Contact,
                    distributorEntity.Id) : null;

                var AddressInfo = addressInfo != null ? new AddressInfoEntity(
                    addressInfo.AddressType,
                    addressInfo.Address,
                    distributorEntity.Id) : null;

                var DistributorDetails = new DistributorInfoEntities(
                    distributorEntity,
                    IdentityCardInfo,
                    ContactInfo,
                    AddressInfo);

                var result = await _distributorsRepository.AddDistributorInfo(DistributorDetails);
                return result;
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
};
