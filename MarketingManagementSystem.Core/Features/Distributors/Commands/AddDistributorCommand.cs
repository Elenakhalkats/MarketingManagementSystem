using AutoMapper;
using MarketingManagementSystem.Core.Entities;
using MarketingManagementSystem.Core.Interfaces;
using MarketingManagementSystem.Core.ResponseModels;
using MediatR;

namespace MarketingManagementSystem.Web.Features.Distributors.Commands;

public sealed record AddDistributorCommand(AddDistributorInfo AddDistributorInfo) : IRequest<int>
{
    public class AddDistributorCommandHandler : IRequestHandler<AddDistributorCommand, int>
    {
        private readonly IDistributorsRepository _distributorsRepository;
        private readonly IMapper _mapper;
        public AddDistributorCommandHandler(IDistributorsRepository distributorsRepository, IMapper mapper)
        {
            _distributorsRepository = distributorsRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(AddDistributorCommand request, CancellationToken cancellationToken)
        {
            var distributor = request.AddDistributorInfo.Distributor;

            var Distributor = distributor != null ? new DistributorEntity(distributor) : null;
            
            var distributorEntity = await _distributorsRepository.AddDistributor(Distributor);
            var distributorId = distributorEntity.Id;

            var identityCardInfo = request.AddDistributorInfo.IdentityCardInfo;
            var contactInfo = request.AddDistributorInfo.ContactInfo;
            var addressInfo = request.AddDistributorInfo.AddressInfo;

            var IdentityCardInfo = identityCardInfo != null ? new IdentityCardInfoEntity(identityCardInfo, distributorId) : null;
            var ContactInfo = contactInfo != null ? new ContactInfoEntity(contactInfo, distributorId) : null;
            var AddressInfo = addressInfo != null ? new AddressInfoEntity(addressInfo, distributorId) : null;

            var DistributorDetails = new DistributorInfoEntities(distributorEntity, IdentityCardInfo, ContactInfo, AddressInfo);

            var result = await _distributorsRepository.AddDistributorInfo(DistributorDetails, distributorId);
            return result;
        }
    }
};
