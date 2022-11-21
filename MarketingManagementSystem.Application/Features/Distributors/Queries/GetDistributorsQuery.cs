using AutoMapper;
using MarketingManagementSystem.Application.Interfaces;
using MarketingManagementSystem.Application.ResponseModels;
using MarketingManagementSystem.Domain.Entities;
using MediatR;

namespace MarketingManagementSystem.Application.Features.Distributors.Queries;

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
            var distributors = await _distributorsRepository.GetDistributorsAsync(Filter);

            var result = _mapper.Map<List<Distributor>>(distributors);
            return result;
        }
    }
}

