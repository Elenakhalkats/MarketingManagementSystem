using AutoMapper;
using MarketingManagementSystem.Core.Entities;
using MarketingManagementSystem.Core.Interfaces;
using MarketingManagementSystem.Core.ResponseModels;
using MediatR;

namespace MarketingManagementSystem.Core.Features.Distributors.Commands;

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

            var DistributorSales = GetDistributorsWithCountedSales(sales);

            var recommendations = _distributorsRepository.GetRecommendations().Result;
            var bonuses = CountBonuses(StartDate, EndDate, recommendations, DistributorSales);

            await _distributorsRepository.AddBonuses(bonuses);

            foreach (var sale in sales)
            {
                sale.Counted = true;
            }
            await _productSalesRepository.UpdateSales(sales);

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
        private static List<DistributorSalesFields> GetDistributorsWithCountedSales(List<SaleEntity> sales)
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
        private static List<BonusEntity> CountBonuses(DateTime startDate, DateTime endDate, List<DistributorRecommendationEntity> recommendations, List<DistributorSalesFields> distributorSale)
        {
            var bonuses = new List<BonusEntity>();

            foreach (var distributor in distributorSale)
            {
                float bonus = 0;
                bonus += distributor.CountedTotal / 10;

                var recommendToD = recommendations.FindAll(x => x.Recommendator == distributor.DistributorId).ToList();
                if (recommendToD.Count() != 0)
                {
                    foreach (var recommendTo in recommendToD)
                    {
                        var sale = distributorSale.FirstOrDefault(x => x.DistributorId == recommendTo.RecommendTo);
                        if (sale != null)
                        {
                            bonus += sale.CountedTotal / 20;

                            var recommendToDD = recommendations.FindAll(x => x.Recommendator == recommendTo.RecommendTo).ToList();
                            if (recommendToDD.Count() != 0)
                            {
                                foreach (var recommendTo1 in recommendToDD)
                                {
                                    var sale1 = distributorSale.FirstOrDefault(x => x.DistributorId == recommendTo.RecommendTo);
                                    if (sale1 != null)
                                    {
                                        bonus += sale1.CountedTotal / 20;
                                    }
                                }
                            }
                        }
                    }
                }
                var bonusEntity = new BonusEntity(distributor.DistributorId, startDate, endDate, bonus);
                bonuses.Add(bonusEntity);
            }
            return bonuses;
        }
    }
}
