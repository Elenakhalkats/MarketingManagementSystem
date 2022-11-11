using AutoMapper;
using MarketingManagementSystem.Core.Interfaces;
using MarketingManagementSystem.Core.ResponseModels;
using MarketingManagementSystem.Core.Entities;
using MediatR;

namespace MarketingManagementSystem.Core.Features.Distributors.Queries;

public sealed record GetDistributorsQuery(
    string? FirstName,
    string? LastName
    ) : IRequest<List<Distributor>>
{
    public class GetDistributorsQueryHandler : IRequestHandler<GetDistributorsQuery, List<Distributor>>
    {
        private readonly IDistributorsRepository _distributorsRepository;
        private readonly IMapper _mapper;
        public GetDistributorsQueryHandler(
            IDistributorsRepository distributorsRepository,
            IMapper mapper)
        {
            _distributorsRepository = distributorsRepository;
            _mapper = mapper;
        }
        public async Task<List<Distributor>> Handle(GetDistributorsQuery request, CancellationToken cancellationToken)
        {
            var Filter = new DistributorsFilterObjects(request.FirstName ?? null, request.LastName ?? null);
            var distributors = await _distributorsRepository.GetDistributors(Filter);

            var result = _mapper.Map<List<Distributor>>(distributors);
            return result;
        }
    }
}

