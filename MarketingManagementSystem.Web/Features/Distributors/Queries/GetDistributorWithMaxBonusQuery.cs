﻿using AutoMapper;
using MarketingManagementSystem.SharedKernel.Interfaces;
using MarketingManagementSystem.Web.Models;
using MediatR;

namespace MarketingManagementSystem.Web.Features.Distributors.Queries;

public sealed class GetDistributorWithMaxBonusQuery : IRequest<Distributor>
{
    public class GetDistributorWithMaxBonusQueryHandler : IRequestHandler<GetDistributorWithMaxBonusQuery, Distributor>
    {
        private readonly IDistributorsRepository _distributorsRepository;
        private readonly IMapper _mapper;
        public GetDistributorWithMaxBonusQueryHandler(
            IDistributorsRepository distributorsRepository,
            IMapper mapper)
        {
            _distributorsRepository = distributorsRepository;
            _mapper = mapper;
        }

        public async Task<Distributor> Handle(GetDistributorWithMaxBonusQuery request, CancellationToken cancellationToken)
        {
            var distributor = await _distributorsRepository.GetDistributorWithMaxBonus();
            var result = _mapper.Map<Distributor>(distributor);
            return result;  
        }
    }
}

