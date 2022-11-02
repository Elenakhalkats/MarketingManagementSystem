using AutoMapper;
using MarketingManagementSystem.SharedKernel.Interfaces;
using MarketingManagementSystem.Web.Models;
using MediatR;

namespace MarketingManagementSystem.Web.Features.Distributors.Queries;

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
            var distributor = await _distributorsRepository.GetDistributorWithMinBonus();
            var result = _mapper.Map<Distributor>(distributor);

            return result;
        }
    }
}

