using MarketingManagementSystem.Application.Interfaces;
using MediatR;

namespace MarketingManagementSystem.Application.Features.Distributors.Commands;

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

            await _distributorsRepository.RecommendDistributorAsync(recommendatorId, recommendToId);
            return Unit.Value;
        }
    }
}
