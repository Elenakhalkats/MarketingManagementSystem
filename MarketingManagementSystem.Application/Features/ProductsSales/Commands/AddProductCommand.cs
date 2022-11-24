using MarketingManagementSystem.Application.Interfaces;
using MarketingManagementSystem.Domain.Entities;
using MediatR;

namespace MarketingManagementSystem.Application.Features.ProductsSales.Commands;

public sealed record AddProductCommand(
    string ProductCode,
    string ProductName,
    float UnitPrice) : IRequest<int>
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, int>
    {
        private readonly IProductsSalesRepository _productsSalesRepository;
        public AddProductCommandHandler(IProductsSalesRepository productsSalesRepository)
        {
            _productsSalesRepository = productsSalesRepository;
        }

        public async Task<int> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var newProduct = new ProductEntity(request.ProductCode, request.ProductName, request.UnitPrice);
            var result = await _productsSalesRepository.AddProductAsync(newProduct);
            return result;
        }
    }
}
