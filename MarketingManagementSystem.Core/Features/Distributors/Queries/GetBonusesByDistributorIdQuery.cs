using AutoMapper;
using MarketingManagementSystem.Core.Interfaces;
using MarketingManagementSystem.Core.ResponseModels;
using MarketingManagementSystem.Core.Entities;
using MediatR;

namespace MarketingManagementSystem.Core.Features.Distributors.Queries;

public sealed record GetBonusesByDistributorIdQuery : IRequest<DistributorBonus>
{
    public int Id { get; set; }
    public class GetBonusesByDistributorIdQueryHandler : IRequestHandler<GetBonusesByDistributorIdQuery, DistributorBonus>
    {
        private readonly IDistributorsRepository _distributorsRepository;
        private readonly IMapper _mapper;
        public GetBonusesByDistributorIdQueryHandler(
            IDistributorsRepository distributorsRepository,
            IMapper mapper)
        {
            _distributorsRepository = distributorsRepository;
            _mapper = mapper;
        }
        public async Task<DistributorBonus> Handle(GetBonusesByDistributorIdQuery request, CancellationToken cancellationToken)
        {
            var distributorBonus = await _distributorsRepository.GetBonusesByDistributorId(request.Id);
            var distributor = _mapper.Map<Distributor>(distributorBonus.Distributor);

            var bonusEntities = distributorBonus.BonusEntities;
            var bonuses = _mapper.Map<List<Bonus>>(bonusEntities);
            var result = new DistributorBonus(distributor, bonuses);

            return result;
        }
    }
}