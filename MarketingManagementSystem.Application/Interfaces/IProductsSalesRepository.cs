using MarketingManagementSystem.Application.ResponseModels;
using MarketingManagementSystem.Domain.Entities;

namespace MarketingManagementSystem.Application.Interfaces;

public interface IProductsSalesRepository
{
    Task<ProductEntity> GetProductByIdAsync(int Id);
    Task<int> AddProductAsync(ProductEntity Product);
    Task<List<SaleEntity>> GetSalesAsync(SalesFilterObjects? salesFilterObjects);
    Task<int> AddSaleAsync(SaleEntity Sale);
    Task<bool> UpdateSalesAsync(List<SaleEntity> SalesToUpdate);
}
