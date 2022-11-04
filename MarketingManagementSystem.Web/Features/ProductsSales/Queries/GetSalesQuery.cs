﻿using AutoMapper;
using MarketingManagementSystem.Core.Models;
using MarketingManagementSystem.SharedKernel.Interfaces;
using MarketingManagementSystem.Web.Models;
using MediatR;

namespace MarketingManagementSystem.Web.Features.ProductsSales.Queries;

public record GetSalesQuery(
    int? DistributorId,
    DateTime? StartDate,
    DateTime? EndDate,
    int? ProductId) : IRequest<List<Sale>>
{
    public class GetSalesQueryHandler : IRequestHandler<GetSalesQuery, List<Sale>>
    {
        private readonly IProductsSalesRepository _productsSalesRepository;
        private readonly IMapper _mapper;
        public GetSalesQueryHandler(
            IProductsSalesRepository productsSalesRepository,
            IMapper mapper)
        {
            _productsSalesRepository = productsSalesRepository;
            _mapper = mapper;
        }
        public async Task<List<Sale>> Handle(GetSalesQuery request, CancellationToken cancellationToken)
        {
            var Filter = new SalesFilterObjects(request.DistributorId ?? null, request.StartDate ?? null, request.EndDate ?? null, request.ProductId ?? null);
            var sales = await _productsSalesRepository.GetSales(Filter);
       
            var result = _mapper.Map<List<Sale>>(sales);
            return result;
        }
    }
}
