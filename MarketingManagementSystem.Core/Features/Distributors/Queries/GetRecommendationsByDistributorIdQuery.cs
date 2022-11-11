using AutoMapper;
using MarketingManagementSystem.Core.Interfaces;
using MarketingManagementSystem.Core.Entities;
using MediatR;

namespace MarketingManagementSystem.Core.Features.Distributors.Queries;

public sealed record GetRecommendationsByDistributorIdQuery : IRequest<List<Distributor>>
{
    public int Id { get; set; }
    public class GetRecommendationsByDistributorIdQueryHandler : IRequestHandler<GetRecommendationsByDistributorIdQuery, List<Distributor>>
    {
        private readonly IDistributorsRepository _distributorsRepository;
        private readonly IMapper _mapper;
        public GetRecommendationsByDistributorIdQueryHandler(
            IDistributorsRepository distributorsRepository,
            IMapper mapper)
        {
            _distributorsRepository = distributorsRepository;
            _mapper = mapper;
        }

        public async Task<List<Distributor>> Handle(GetRecommendationsByDistributorIdQuery request, CancellationToken cancellationToken)
        {
            var distributorEntities = await _distributorsRepository.GetRecommendationsById(request.Id);
            var result = _mapper.Map<List<Distributor>>(distributorEntities);
            return result;
        }
    }
}