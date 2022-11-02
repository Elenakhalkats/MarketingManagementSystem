using MarketingManagementSystem.Core.Entities;
using MarketingManagementSystem.SharedKernel.Interfaces;
using MediatR;

namespace MarketingManagementSystem.Web.Features.ProductsSales.Commands;

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
            await _distributorsRepository.GetDistributorById(request.DistributorId);
            var newSale = new SaleEntity(request.DistributorId, request.Date, request.ProductId, request.UnitPrice, request.TotalPrice);
            await _productsSalesRepository.AddSale(newSale);

            return Unit.Value;
        }
    }
}