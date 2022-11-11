using AutoMapper;
using MarketingManagementSystem.Core.Interfaces;
using MarketingManagementSystem.Core.Entities;
using MediatR;

namespace MarketingManagementSystem.Core.Features.Distributors.Queries;

public sealed record GetDistributorByIdQuery : IRequest<DistributorInfo>
{
    public int Id { get; set; }
    public class GetDistributorByIdQueryHandler : IRequestHandler<GetDistributorByIdQuery, DistributorInfo>
    {
        private readonly IDistributorsRepository _distributorsRepository;
        private readonly IMapper _mapper;
        public GetDistributorByIdQueryHandler(
            IDistributorsRepository distributorsRepository,
            IMapper mapper)
        {
            _distributorsRepository = distributorsRepository;
            _mapper = mapper;
        }
        public async Task<DistributorInfo> Handle(GetDistributorByIdQuery request, CancellationToken cancellationToken)
        {
            var distributorInfo = await _distributorsRepository.GetDistributorInfoById(request.Id);

            var Distributor = distributorInfo.DistributorInfo;
            var IdentityCardInfo = distributorInfo.IdentityCardInfo;
            var ContactInfo = distributorInfo.ContactInfo;
            var AddressInfo = distributorInfo.AddressInfo;

            var distributor = Distributor != null ? _mapper.Map<Distributor>(Distributor) : null;
            var identityCardInfo = IdentityCardInfo != null ? _mapper.Map<IdentityCardInfo>(IdentityCardInfo) : null;
            var contactInfo = ContactInfo != null ? _mapper.Map<ContactInfo>(ContactInfo) : null;
            var addressInfo = AddressInfo != null ? _mapper.Map<AddressInfo>(AddressInfo) : null;

            var distributorDetails = new DistributorInfo(distributor, identityCardInfo, contactInfo, addressInfo);
            return distributorDetails;
        }
    }
};
