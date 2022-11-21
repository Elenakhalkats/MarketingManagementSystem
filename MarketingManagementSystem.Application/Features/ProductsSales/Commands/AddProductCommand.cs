using MarketingManagementSystem.Application.Interfaces;
using MarketingManagementSystem.Domain.Entities;
using MediatR;

namespace MarketingManagementSystem.Application.Features.ProductsSales.Commands;

public sealed record AddProductCommand(
    string ProductCode,
    string ProductName,
    float UnitPrice) : IRequest<Unit>
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand>
    {
        private readonly IProductsSalesRepository _productsSalesRepository;
        public AddProductCommandHandler(IProductsSalesRepository productsSalesRepository)
        {
            _productsSalesRepository = productsSalesRepository;
        }

        public async Task<Unit> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var newProduct = new ProductEntity(request.ProductCode, request.ProductName, request.UnitPrice);
            await _productsSalesRepository.AddProductAsync(newProduct);
            return Unit.Value;
        }
    }
}
