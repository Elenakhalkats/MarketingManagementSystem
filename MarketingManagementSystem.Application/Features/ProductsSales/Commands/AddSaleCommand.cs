using MarketingManagementSystem.Application.Interfaces;
using MarketingManagementSystem.Domain.Entities;
using MediatR;

namespace MarketingManagementSystem.Application.Features.ProductsSales.Commands;

public sealed record AddSaleCommand(
    int DistributorId,
    DateTime Date,
    int ProductId,
    float UnitPrice,
    float TotalPrice) : IRequest<int>
{
    public class AddSaleCommandHandler : IRequestHandler<AddSaleCommand,int>
    {
        private readonly IProductsSalesRepository _productsSalesRepository;
        private readonly IDistributorsRepository _distributorsRepository;
        public AddSaleCommandHandler(IProductsSalesRepository productsSalesRepository, IDistributorsRepository distributorsRepository)
        {
            _productsSalesRepository = productsSalesRepository;
            _distributorsRepository = distributorsRepository;
        }

        public async Task<int> Handle(AddSaleCommand request, CancellationToken cancellationToken)
        {
            var distributor = await _distributorsRepository.GetDistributorByIdAsync(request.DistributorId);
            var product = await _productsSalesRepository.GetProductByIdAsync(request.ProductId);
            var newSale = new SaleEntity(distributor.Id, request.Date, product.Id, request.UnitPrice, request.TotalPrice);
            var result = await _productsSalesRepository.AddSaleAsync(newSale);
            return result;
        }
    }
}