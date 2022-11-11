using MarketingManagementSystem.Core.Entities;
using MarketingManagementSystem.Core.ResponseModels;

namespace MarketingManagementSystem.Core.Interfaces;

public interface IProductsSalesRepository
{
    Task<ProductEntity> GetProductById(int Id);
    Task<bool> AddProduct(ProductEntity Product);
    Task<List<SaleEntity>> GetSales(SalesFilterObjects? salesFilterObjects);
    Task<bool> AddSale(SaleEntity Sale);
    Task<bool> UpdateSales(List<SaleEntity> SalesToUpdate);
}
