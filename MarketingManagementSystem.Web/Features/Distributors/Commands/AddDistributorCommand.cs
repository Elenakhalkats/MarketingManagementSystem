using AutoMapper;
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
        private readonly IMapper _mapper;
        public AddDistributorCommandHandler(IDistributorsRepository distributorsRepository, IMapper mapper)
        {
            _distributorsRepository = distributorsRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(AddDistributorCommand request, CancellationToken cancellationToken)
        {
            var distributor = request.DistributorInfo.Distributor;

            var Distributor = distributor != null ? _mapper.Map<DistributorEntity>(distributor) : null;
            var distributorEntity = await _distributorsRepository.AddDistributor(Distributor);
            var distributorId = distributorEntity.Id;

            var identityCardInfo = request.DistributorInfo.IdentityCardInfo;
            var contactInfo = request.DistributorInfo.ContactInfo;
            var addressInfo = request.DistributorInfo.AddressInfo;

            if (identityCardInfo != null) identityCardInfo.DistributorId = distributorId;
            if (contactInfo != null) contactInfo.DistributorId = distributorId;
            if (addressInfo != null) addressInfo.DistributorId = distributorId;

            var IdentityCardInfo = identityCardInfo != null ? _mapper.Map<IdentityCardInfoEntity>(identityCardInfo) : null;
            var ContactInfo = contactInfo != null ? _mapper.Map<ContactInfoEntity>(contactInfo) : null;
            var AddressInfo = addressInfo != null ? _mapper.Map<AddressInfoEntity>(addressInfo) : null;

            var DistributorDetails = new DistributorInfoEntities(distributorEntity, IdentityCardInfo, ContactInfo, AddressInfo);

            var result = await _distributorsRepository.AddDistributorInfo(DistributorDetails, distributorId);
            return result;
        }
    }
};
