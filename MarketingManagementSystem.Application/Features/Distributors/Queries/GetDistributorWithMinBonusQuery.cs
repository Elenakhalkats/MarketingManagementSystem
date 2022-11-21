using AutoMapper;
using MarketingManagementSystem.Application.Interfaces;
using MarketingManagementSystem.Domain.Entities;
using MediatR;

namespace MarketingManagementSystem.Application.Features.Distributors.Queries;

public sealed class GetDistributorWithMinBonusQuery : IRequest<Distributor>
{
    public class GetDistributorWithMinBonusQueryHandler : IRequestHandler<GetDistributorWithMinBonusQuery, Distributor>
    {
        private readonly IDistributorsRepository _distributorsRepository;
        private readonly IMapper _mapper;
        public GetDistributorWithMinBonusQueryHandler(
            IDistributorsRepository distributorsRepository,
            IMapper mapper)
        {
            _distributorsRepository = distributorsRepository;
            _mapper = mapper;
        }

        public async Task<Distributor> Handle(GetDistributorWithMinBonusQuery request, CancellationToken cancellationToken)
        {
            var distributor = await _distributorsRepository.GetMinBonusAsync();
            var result = _mapper.Map<Distributor>(distributor);

            return result;
        }
    }
}

