using AutoMapper;
using MarketingManagementSystem.Application.Exceptions;
using MarketingManagementSystem.Application.Interfaces;
using MarketingManagementSystem.Application.ResponseModels;
using MarketingManagementSystem.Domain.Entities;
using MediatR;
using System;

namespace MarketingManagementSystem.Application.Features.Distributors.Commands;

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

            if (StartDate == default) StartDate = DateTime.MinValue;
            if (EndDate == default) EndDate = DateTime.Now;

            var Filter = new SalesFilterObjects(null, StartDate, EndDate, null);
            var sales = await _productSalesRepository.GetSalesAsync(Filter);

            var DistributorSales = GetDistributorsWithCountedSales(sales);

            var recommendations = _distributorsRepository.GetRecommendationsAsync().Result;
            var bonuses = CountBonuses(StartDate, EndDate, recommendations, DistributorSales);

            await _distributorsRepository.AddBonusesAsync(bonuses);

            foreach (var sale in sales)
            {
                sale.Counted = true;
            }
            await _productSalesRepository.UpdateSalesAsync(sales);

            var result = new List<DistributorCountedBonus>();
            var distributorss = await _distributorsRepository.GetDistributorsAsync(null);
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
            var distributorIds = sales.DistinctBy(x => x.DistributorId).Select(x => x.DistributorId);
            var DistributorSales = new List<DistributorSalesFields>();

            foreach (var distributorId in distributorIds)
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
        private static List<BonusEntity> CountBonuses(DateTime startDate, DateTime endDate, List<RecommendationEntity> recommendations, List<DistributorSalesFields> distributorSale)
        {
            var bonuses = new List<BonusEntity>();

            foreach (var distributor in distributorSale) 
            {
                float bonus = 0;
                bonus += distributor.CountedTotal / 10; 
                var recommendToD = recommendations.FindAll(x => x.RecommendatorId == distributor.DistributorId).ToList();
                if (recommendToD.Count != 0)
                {
                    foreach (var recommendTo in recommendToD)
                    {
                        var sale = distributorSale.FirstOrDefault(x => x.DistributorId == recommendTo.RecommendedId);
                        if (sale != null)
                        {
                            bonus += sale.CountedTotal / 20; 

                            var recommendToDD = recommendations.FindAll(x => x.RecommendatorId == recommendTo.RecommendedId).ToList();
                            if (recommendToDD.Count != 0)
                            {
                                foreach (var recommendTo1 in recommendToDD)
                                {
                                    var sale1 = distributorSale.FirstOrDefault(x => x.DistributorId == recommendTo1.RecommendedId);
                                    if (sale1 != null)
                                    {
                                        bonus += sale1.CountedTotal / 100; 
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
