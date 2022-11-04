using MarketingManagementSystem.SharedKernel.Interfaces;
using MediatR;

namespace MarketingManagementSystem.Web.Features.Distributors.Commands;

public sealed record DeleteDistributorCommand : IRequest<bool>
{
    public int Id { get; set; }
    public class DeleteDistributorCommandHandler : IRequestHandler<DeleteDistributorCommand, bool>
    {
        private readonly IDistributorsRepository _distributorsRepository;
        public DeleteDistributorCommandHandler(IDistributorsRepository distributorsRepository)
        {
            _distributorsRepository = distributorsRepository;
        }
        public async Task<bool> Handle(DeleteDistributorCommand request, CancellationToken cancellationToken)
        {
            var distributorId = request.Id;
            await _distributorsRepository.DeleteDistributor(distributorId);
            return true;
        }
    }
}
