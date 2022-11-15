using AutoMapper;
using MarketingManagementSystem.Core.Interfaces;
using MarketingManagementSystem.Core.Entities;
using MediatR;

namespace MarketingManagementSystem.Core.Features.Distributors.Queries;

public sealed class GetDistributorWithMaxBonusQuery : IRequest<Distributor>
{
    public class GetDistributorWithMaxBonusQueryHandler : IRequestHandler<GetDistributorWithMaxBonusQuery, Distributor>
    {
        private readonly IDistributorsRepository _distributorsRepository;
        private readonly IMapper _mapper;
        public GetDistributorWithMaxBonusQueryHandler(
            IDistributorsRepository distributorsRepository,
            IMapper mapper)
        {
            _distributorsRepository = distributorsRepository;
            _mapper = mapper;
        }

        public async Task<Distributor> Handle(GetDistributorWithMaxBonusQuery request, CancellationToken cancellationToken)
        {
            var distributor = await _distributorsRepository.GetMaxBonus();
            var result = _mapper.Map<Distributor>(distributor);

            return result;
        }
    }
}

