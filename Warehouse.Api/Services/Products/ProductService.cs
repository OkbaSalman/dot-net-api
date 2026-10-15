using Warehouse.Api.Data;
using Warehouse.Api.Data.Entities;
using Warehouse.Api.DTOs.Requests;
using Warehouse.Api.DTOs.Responses;
using Microsoft.EntityFrameworkCore;

namespace Warehouse.Api.Services.Products;
public class ProductService : IProductService
{
    private readonly WarehouseDbContext _db;
    private readonly ILogger<ProductService> _logger;

    public ProductService(WarehouseDbContext db, ILogger<ProductService> logger)
    {
        _db = db;
        _logger = logger;
    }
    public async Task<ProductResponse> CreateProductAsync(CreateProductRequest request)
    {
        var product = new Product
        {
            Name = request.Name,
            Quantity = request.Quantity,
            Price = request.Price
        };
        _db.Products.Add(product);
        await _db.SaveChangesAsync();
        _logger.LogInformation("Created product {ProductId}", product.Id);
        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Quantity = product.Quantity,
            Price = product.Price
        };
    }


    public async Task<List<ProductResponse>> GetProductsAsync()
    {
        var products = await _db.Products.ToListAsync();
        var result = new List<ProductResponse>();

        foreach (var p in products)
        {
            result.Add(new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Quantity = p.Quantity,
                Price = p.Price
            });
        }

        return result;
    }

    public async Task<ProductResponse?> GetProductByIdAsync(int id)
    {
        var product = await _db.Products.FindAsync(id);
        if (product is null){
            _logger.LogWarning("Product {ProductId} was not found", id);
            return null;
        }
        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Quantity = product.Quantity,
            Price = product.Price
        };
    }

    public async Task<ProductResponse?> UpdateProductAsync(int id, UpdateProductRequest request)
    {
     var product = await _db.Products.FindAsync(id);
     if (product is null)
     {
        _logger.LogWarning("Product {ProductId} was not found", id);
        return null;
     }
    product.Name = request.Name;
    product.Quantity = request.Quantity;
    product.Price = request.Price;

    await _db.SaveChangesAsync();
    _logger.LogInformation("Updated product {ProductId}", id);

    return new ProductResponse
    {
        Id = product.Id,
        Name = product.Name,
        Quantity = product.Quantity,
        Price = product.Price
    };
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await _db.Products.FindAsync(id);
        if (product is null)
        {
            _logger.LogWarning("Product {ProductId} was not found", id);
            return false;
        }
        
        _db.Products.Remove(product);
        await _db.SaveChangesAsync();
        _logger.LogInformation("Deleted product {ProductId}", id);
        
        return true;
    }
}