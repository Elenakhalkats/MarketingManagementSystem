using AutoMapper;
using MarketingManagementSystem.Core.Interfaces;
using MarketingManagementSystem.Core.Entities;
using MediatR;

namespace MarketingManagementSystem.Core.Features.ProductsSales.Queries;

public sealed record GetProductByIdQuery : IRequest<Product>
{
    public int Id { get; set; }
    public class GetProductByIdQueryQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly IProductsSalesRepository _productsSalesRepository;
        private readonly IMapper _mapper;
        public GetProductByIdQueryQueryHandler(IProductsSalesRepository productsSalesRepository,IMapper mapper)
        {
            _productsSalesRepository = productsSalesRepository;
            _mapper = mapper;
        }
        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productsSalesRepository.GetProductById(request.Id);
            var result = _mapper.Map<Product>(product);

            return result;
        }
    }
}
