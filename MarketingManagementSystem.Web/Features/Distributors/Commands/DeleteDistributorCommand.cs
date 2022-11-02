using MarketingManagementSystem.SharedKernel.Interfaces;
using MediatR;

namespace MarketingManagementSystem.Web.Features.Distributors.Commands;

public sealed record DeleteDistributorCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public class DeleteDistributorCommandHandler : IRequestHandler<DeleteDistributorCommand>
    {
        private readonly IDistributorsRepository _distributorsRepository;
        public DeleteDistributorCommandHandler(IDistributorsRepository distributorsRepository)
        {
            _distributorsRepository = distributorsRepository;
        }
        public async Task<Unit> Handle(DeleteDistributorCommand request, CancellationToken cancellationToken)
        {
            var distributorId = request.Id;
            await _distributorsRepository.DeleteDistributor(distributorId);
            return Unit.Value;
        }
    }
}
