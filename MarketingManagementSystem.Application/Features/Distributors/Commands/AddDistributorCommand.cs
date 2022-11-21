using AutoMapper;
using MarketingManagementSystem.Application.Interfaces;
using MarketingManagementSystem.Application.ResponseModels;
using MarketingManagementSystem.Domain.Entities;
using MediatR;

namespace MarketingManagementSystem.Application.Features.Distributors.Commands;

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
            var distributor = new DistributorEntity(request.AddDistributorInfo.Distributor);
            
            var distributorEntity = await _distributorsRepository.AddDistributorAsync(distributor);
            var distributorId = distributorEntity.Id;

            var identityCardInfo = request.AddDistributorInfo.IdentityCardInfo;
            var contactInfo = request.AddDistributorInfo.ContactInfo;
            var addressInfo = request.AddDistributorInfo.AddressInfo;

            var distributorDetails = new DistributorInfoEntities(
                distributorEntity,
                identityCardInfo: identityCardInfo != null ? new IdentityCardInfoEntity(identityCardInfo, distributorId) : null,
                contactInfo: contactInfo != null ? new ContactInfoEntity(contactInfo, distributorId) : null,
                addressInfo: addressInfo != null ? new AddressInfoEntity(addressInfo, distributorId) : null);

            var result = await _distributorsRepository.AddDistributorInfoAsync(distributorDetails, distributorId);
            return result;
        }
    }
};
