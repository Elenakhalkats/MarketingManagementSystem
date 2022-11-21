using MarketingManagementSystem.Application.Interfaces;
using MarketingManagementSystem.Domain.Entities;
using MediatR;

namespace MarketingManagementSystem.Application.Features.ProductsSales.Commands;

public sealed record AddSaleCommand(
    int DistributorId,
    DateTime Date,
    int ProductId,
    float UnitPrice,
    float TotalPrice) : IRequest<Unit>
{
    public class AddSaleCommandHandler : IRequestHandler<AddSaleCommand>
    {
        private readonly IProductsSalesRepository _productsSalesRepository;
        private readonly IDistributorsRepository _distributorsRepository;
        public AddSaleCommandHandler(IProductsSalesRepository productsSalesRepository, IDistributorsRepository distributorsRepository)
        {
            _productsSalesRepository = productsSalesRepository;
            _distributorsRepository = distributorsRepository;
        }

        public async Task<Unit> Handle(AddSaleCommand request, CancellationToken cancellationToken)
        {
            await _distributorsRepository.GetDistributorByIdAsync(request.DistributorId);

            var newSale = new SaleEntity(request.DistributorId, request.Date, request.ProductId, request.UnitPrice, request.TotalPrice);
            await _productsSalesRepository.AddSaleAsync(newSale);

            return Unit.Value;
        }
    }
}