using MarketingManagementSystem.Application.Interfaces;
using MarketingManagementSystem.Application.ResponseModels;
using MarketingManagementSystem.Domain.Entities;
using MarketingManagementSystem.Infrastucture.Contexts;

namespace MarketingManagementSystem.Infrastucture.Repositories;

public class ProductsSalesRepository : IProductsSalesRepository
{
    private readonly MarketingManagementSystemContext _context;

    public ProductsSalesRepository(MarketingManagementSystemContext context)
    {
        _context = context;
    }
    public async Task<bool> AddProductAsync(ProductEntity Product)
    {
        _context.Products.Add(Product);
        _context.SaveChanges();
        return true;
    }

    public async Task<bool> AddSaleAsync(SaleEntity Sale)
    {
        _context.Sales.Add(Sale);
        _context.SaveChanges();
        return true;
    }

    public async Task<ProductEntity> GetProductByIdAsync(int Id)
    {
        var product = _context.Products.FirstOrDefault(x => x.Id == Id);
        if (product == null) throw new Exception();
        return product;
    }

    public async Task<List<SaleEntity>> GetSalesAsync(SalesFilterObjects? salesFilterObjects)
    {
        var sales = _context.Sales.Where(x => x.Counted != true).ToList();

        if (salesFilterObjects != null)
        {
            if (salesFilterObjects.StartDate != null) 
                sales = (List<SaleEntity>)sales.FindAll(x => x.Date > salesFilterObjects.StartDate);
            if (salesFilterObjects.EndDate != null)
                sales = (List<SaleEntity>)sales.FindAll(x => x.Date < salesFilterObjects.EndDate);
            if (salesFilterObjects.DistributorId != null) sales = (List<SaleEntity>)sales.FindAll(x => x.DistributorId == salesFilterObjects.DistributorId);
            if (salesFilterObjects.ProductId != null) sales = (List<SaleEntity>)sales.FindAll(x => x.ProductId == salesFilterObjects.ProductId);
            if (salesFilterObjects.Counted != null) sales = (List<SaleEntity>)sales.FindAll(x => x.Counted == salesFilterObjects.Counted);
        }
        return sales;
    }

    public async Task<bool> UpdateSalesAsync(List<SaleEntity> SalesToUpdate)
    {
        _context.UpdateRange(SalesToUpdate);
        _context.SaveChanges();

        return true;
    }
}
