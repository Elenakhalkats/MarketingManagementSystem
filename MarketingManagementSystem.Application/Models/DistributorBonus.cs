﻿//using MarketingManagementSystem.Domain.Entities;
//using System;
//using System.Collections.Generic;
//namespace MarketingManagementSystem.Application.Models;

//public class DistributorBonus
//{
//    public int Distributor get; set; }
//}
//public class DistributorSales
//{
//public DistributorSales(
//    int? id,
//    int distributorId,
//    List<Sale> sales)
//{
//    Id = id;
//    DistributorId = distributorId;
//    CountedTotal = Count(sales);
//}
//public int? Id { get; set; }
//public int DistributorId { get; set; }
//public float CountedTotal { get; set; }
//private float Count(List<Sale> sales)
//{
//    float count = 0;
//    foreach (Sale sale in sales)
//    {
//        count += sale.TotalPrice;
//    }
//    return count;
//}
//}
