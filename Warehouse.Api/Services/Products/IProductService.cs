using Warehouse.Api.DTOs.Requests;
using Warehouse.Api.DTOs.Responses;

namespace Warehouse.Api.Services.Products;

public interface IProductService
{
    Task<ProductResponse> CreateProductAsync(CreateProductRequest request);

    Task<List<ProductResponse>> GetProductsAsync();

    Task<ProductResponse?> GetProductByIdAsync(int id);

    Task<ProductResponse?> UpdateProductAsync(int id, UpdateProductRequest request);

    Task<bool> DeleteProductAsync(int id);
}