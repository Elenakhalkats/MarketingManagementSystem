using AutoMapper;
using MarketingManagementSystem.Application.Exceptions;
using MarketingManagementSystem.Application.Interfaces;
using MarketingManagementSystem.Application.Models;
using MediatR;

namespace MarketingManagementSystem.Application.Features.Distributors.Queries;

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
            var distributorInfo = await _distributorsRepository.GetDistributorInfoByIdAsync(request.Id);

            if (distributorInfo == null) throw new DistributorNotFoundException();

            var distributor = distributorInfo.DistributorInfo != null ? _mapper.Map<Distributor>(distributorInfo.DistributorInfo) : null;
            var identityCardInfo = distributorInfo.IdentityCardInfo != null ? _mapper.Map<IdentityCardInfo>(distributorInfo.IdentityCardInfo) : null;
            var contactInfo = distributorInfo.ContactInfo != null ? _mapper.Map<ContactInfo>(distributorInfo.ContactInfo) : null;
            var addressInfo = distributorInfo.AddressInfo != null ? _mapper.Map<AddressInfo>(distributorInfo.AddressInfo) : null;

            var distributorDetails = new DistributorInfo(distributor, identityCardInfo, contactInfo, addressInfo);
            return distributorDetails;
        }
    }
};
