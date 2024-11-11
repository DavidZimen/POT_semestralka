using Api.Services.Abstraction;
using AutoMapper;
using Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Policy;

namespace Api.Controllers;

[ApiController]
[Route("products")]
[Authorize]
public class ProductsController : ControllerBase
{

    private readonly ILogger<ProductsController> _logger;
    private readonly IProductService _productService;

    public ProductsController(ILogger<ProductsController> logger, IProductService productService, IMapper mapper)
    {
        _logger = logger;
        _productService = productService;
    }

    [HttpGet]
    [Authorize(Policy = nameof(TesterPolicy))]
    public async Task<ActionResult<ICollection<Product>>> GetProducts()
    {
        _logger.LogInformation("Received call to GET /products");

        var products = await _productService.GetAllProductsAsync();
        
        return products.Count > 0 ? Ok(products) : NotFound("No products found.");
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Product>> GetProduct(Guid id)
    {
        _logger.LogInformation("Received call to GET /products/id");

        var product = await _productService.GetProductByIdAsync(id);

        return Ok(product);
    }

    [HttpPost]
    [Authorize(Policy = nameof(TesterPolicy))]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] Product newProduct)
    {
        _logger.LogInformation("Received call to POST /products");
        
        var productId = await _productService.CreateProductAsync(newProduct);
        newProduct.Id = productId;
        
        return CreatedAtAction(nameof(GetProduct), new { id = productId }, newProduct);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = nameof(UsernamePolicy))]
    public async Task<ActionResult> DeleteProduct(Guid id)
    {
        _logger.LogInformation("Received call to DELETE /products/id");
        
        var result = await _productService.DeleteProductAsync(id);

        return result ? NoContent() : StatusCode(StatusCodes.Status500InternalServerError);
    }
}