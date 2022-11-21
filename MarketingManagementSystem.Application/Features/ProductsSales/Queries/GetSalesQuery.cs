using AutoMapper;
using MarketingManagementSystem.Application.Interfaces;
using MarketingManagementSystem.Application.ResponseModels;
using MarketingManagementSystem.Domain.Entities;
using MediatR;

namespace MarketingManagementSystem.Application.Features.ProductsSales.Queries;

public record GetSalesQuery(
    int? DistributorId,
    DateTime? StartDate,
    DateTime? EndDate,
    int? ProductId) : IRequest<List<Sale>>
{
    public class GetSalesQueryHandler : IRequestHandler<GetSalesQuery, List<Sale>>
    {
        private readonly Interfaces.IProductsSalesRepository _productsSalesRepository;
        private readonly IMapper _mapper;
        public GetSalesQueryHandler(
            IProductsSalesRepository productsSalesRepository,
            IMapper mapper)
        {
            _productsSalesRepository = productsSalesRepository;
            _mapper = mapper;
        }
        public async Task<List<Sale>> Handle(GetSalesQuery request, CancellationToken cancellationToken)
        {
            var Filter = new SalesFilterObjects(request.DistributorId ?? null, request.StartDate ?? null, request.EndDate ?? null, request.ProductId ?? null);
            var sales = await _productsSalesRepository.GetSalesAsync(Filter);
       
            var result = _mapper.Map<List<Sale>>(sales);
            return result;
        }
    }
}
