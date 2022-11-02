using MarketingManagementSystem.Core.Entities;
using MarketingManagementSystem.SharedKernel.Interfaces;
using MediatR;

namespace MarketingManagementSystem.Web.Features.ProductsSales.Commands;

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
            await _productsSalesRepository.AddProduct(newProduct);

            return Unit.Value;
        }
    }
}
