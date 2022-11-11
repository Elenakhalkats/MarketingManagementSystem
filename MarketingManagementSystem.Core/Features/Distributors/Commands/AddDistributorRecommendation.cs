using MarketingManagementSystem.Core.Interfaces;
using MediatR;

namespace MarketingManagementSystem.Core.Features.Distributors.Commands;

public sealed record AddDistributorRecommendations(int RecommendatorId, int RecommendToId) : IRequest<Unit>
{
    public class AddDistributorRecommendationsHandler : IRequestHandler<AddDistributorRecommendations>
    {
        private readonly IDistributorsRepository _distributorsRepository;
        public AddDistributorRecommendationsHandler(IDistributorsRepository distributorsRepository)
        {
            _distributorsRepository = distributorsRepository;
        }
        public async Task<Unit> Handle(AddDistributorRecommendations request, CancellationToken cancellationToken)
        {
            var recommendatorId = request.RecommendatorId;
            var recommendToId = request.RecommendToId;

            await _distributorsRepository.RecommendDistributor(recommendatorId, recommendToId);
            return Unit.Value;
        }
    }
}
