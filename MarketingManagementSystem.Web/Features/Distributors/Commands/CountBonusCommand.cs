using AutoMapper;
using MarketingManagementSystem.Core.Entities;
using MarketingManagementSystem.Core.Models;
using MarketingManagementSystem.SharedKernel.Interfaces;
using MarketingManagementSystem.Web.Models;
using MediatR;

namespace MarketingManagementSystem.Web.Features.Distributors.Commands;

public sealed record CountBonusCommand(
    DateTime StartDate,
    DateTime EndDate) : IRequest<List<DistributorCountedBonus>>
{
    public class CountBonusCommandHandler : IRequestHandler<CountBonusCommand, List<DistributorCountedBonus>>
    {
        private readonly IDistributorsRepository _distributorsRepository;
        private readonly IProductsSalesRepository _productSalesRepository;
        private readonly IMapper _mapper;
        public CountBonusCommandHandler(
            IDistributorsRepository distributorsRepository, 
            IProductsSalesRepository productsSalesRepository, 
            IMapper mapper)
        {
            _distributorsRepository = distributorsRepository;
            _productSalesRepository = productsSalesRepository;  
            _mapper = mapper;
        }
        public async Task<List<DistributorCountedBonus>> Handle(CountBonusCommand request, CancellationToken cancellationToken)
        {
            var StartDate = request.StartDate;
            var EndDate = request.EndDate;

            var Filter = new SalesFilterObjects(null, request.StartDate, request.EndDate, null);
            var sales = await _productSalesRepository.GetSales(Filter);

            //Gets DistributorId with counted total price of its sales 
            var DistributorSales = GetDistributorsWithCountedSales(sales);

            var bonuses = _distributorsRepository.CountBonus(StartDate, EndDate, DistributorSales).Result;

            //Lets Sales "Counted" field be true
            foreach (var sale in sales)
            {
                sale.Counted = true;
            }
            await _productSalesRepository.UpdateSales(sales);

            //Return type List, Distributor with its list of bonuses
            var result = new List<DistributorCountedBonus>();
            var distributorss = await _distributorsRepository.GetDistributors(null);
            foreach (var bonus in bonuses)
            {
                var distributorEntity = distributorss.FirstOrDefault(x => x.Id == bonus.DistributorId);
                var distributorModel = _mapper.Map<Distributor>(distributorEntity);
                var bonusModel = _mapper.Map<Bonus>(bonus);
                result.Add(new DistributorCountedBonus(distributorModel, bonusModel));
            }

            return result;
        }
        private List<DistributorSalesFields> GetDistributorsWithCountedSales(List<SaleEntity> sales)
        {
            var distributorsIds = sales.DistinctBy(x => x.DistributorId).Select(x => x.DistributorId).ToList();

            var DistributorSales = new List<DistributorSalesFields>();

            foreach (var distributorId in distributorsIds)
            {
                var distributorSales = sales.FindAll(x => x.DistributorId == distributorId).ToList();
                float Total = 0;
                foreach (var item in distributorSales)
                {
                    Total += item.TotalPrice;
                }
                DistributorSales.Add(new DistributorSalesFields(distributorId, Total));
            }
            return DistributorSales;
        }
    }
}
