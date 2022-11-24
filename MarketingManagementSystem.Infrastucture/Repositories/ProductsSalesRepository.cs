using MarketingManagementSystem.Application.Exceptions;
using MarketingManagementSystem.Application.Interfaces;
using MarketingManagementSystem.Application.ResponseModels;
using MarketingManagementSystem.Domain.Entities;
using MarketingManagementSystem.Infrastucture.Contexts;
using Microsoft.EntityFrameworkCore;

namespace MarketingManagementSystem.Infrastucture.Repositories;

public class ProductsSalesRepository : IProductsSalesRepository
{
    private readonly MarketingManagementSystemContext _context;

    public ProductsSalesRepository(MarketingManagementSystemContext context)
    {
        _context = context;
    }
    public async Task<int> AddProductAsync(ProductEntity Product)
    {
        var product = await _context.Products.AddAsync(Product); 
        await _context.SaveChangesAsync();

        return product.Entity.Id;
    }

    public async Task<int> AddSaleAsync(SaleEntity Sale)
    {
        var sale = await _context.Sales.AddAsync(Sale);
        await _context.SaveChangesAsync();

        return sale.Entity.Id;
    }

    public async Task<ProductEntity> GetProductByIdAsync(int Id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == Id);
        if (product == null) throw new ProductNotFoundException();
        return product;
    }

    public async Task<List<SaleEntity>> GetSalesAsync(SalesFilterObjects? salesFilterObjects)
    {
        var sales = await _context.Sales.Where(x => !x.Counted).ToListAsync();
        if (sales.Count == 0) throw new SalesNotFoundException();

        if (salesFilterObjects != null)
        {
            if (salesFilterObjects.StartDate != null) 
                sales = sales.FindAll(x => x.Date > salesFilterObjects.StartDate);
            if (salesFilterObjects.EndDate != null)
                sales = sales.FindAll(x => x.Date < salesFilterObjects.EndDate);
            if (salesFilterObjects.DistributorId != null) sales = sales.FindAll(x => x.DistributorId == salesFilterObjects.DistributorId);
            if (salesFilterObjects.ProductId != null) sales = sales.FindAll(x => x.ProductId == salesFilterObjects.ProductId);
            if (salesFilterObjects.Counted != null) sales = sales.FindAll(x => x.Counted == salesFilterObjects.Counted);
        }
        return sales;
    }

    public async Task<bool> UpdateSalesAsync(List<SaleEntity> SalesToUpdate)
    {
        _context.UpdateRange(SalesToUpdate);
        await _context.SaveChangesAsync();
        return true;
    }
}
