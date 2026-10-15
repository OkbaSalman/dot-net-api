using Microsoft.AspNetCore.Mvc;
using Warehouse.Api.DTOs.Requests;
using Warehouse.Api.DTOs.Responses;
using Warehouse.Api.Services.Products;
using Microsoft.AspNetCore.Authorization;

namespace Warehouse.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    
    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ProductResponse>> CreateProduct(CreateProductRequest request)
    {
     var created = await _productService.CreateProductAsync(request);
     return Created($"/api/products/{created.Id}", created);
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductResponse>>> GetProducts()
    {
        var products = await _productService.GetProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponse>> GetProductById(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if(product is null) return NotFound();
        return Ok(product);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<ProductResponse>> UpdateProduct(int id, UpdateProductRequest request)
    {
        var updated = await _productService.UpdateProductAsync(id, request);
        if (updated is null)
        {
            return NotFound();
        }
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var deleted = await _productService.DeleteProductAsync(id);
        if (!deleted)
            return NotFound();
        return NoContent();
    }
}