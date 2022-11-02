using AutoMapper;
using MarketingManagementSystem.SharedKernel.Interfaces;
using MarketingManagementSystem.Web.Models;
using MediatR;

namespace MarketingManagementSystem.Web.Features.Distributors.Queries;

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
            var distributors = new List<Distributor>();

            var distributorEntities = await _distributorsRepository.GetRecommendationsById(request.Id);
            foreach (var distributorEntity in distributorEntities)
            {
                if(distributorEntity != null)
                {
                    var distributor = _mapper.Map<Distributor>(distributorEntity);
                    distributors.Add(distributor);
                }
            }
            return distributors;
        }
    }
}