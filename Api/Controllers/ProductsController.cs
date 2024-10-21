using AutoMapper;
using Domain.Dto;
using Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Security.Policy;

namespace Api.Controllers;

[ApiController]
[Route("products")]
[Authorize]
public class ProductsController : ControllerBase
{

    private readonly ILogger<ProductsController> _logger;
    private readonly ApplicationDbContext _productDbContext;
    private readonly IMapper _mapper;

    public ProductsController(ILogger<ProductsController> logger, ApplicationDbContext productDbContext, IMapper mapper)
    {
        _logger = logger;
        _productDbContext = productDbContext;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetProducts()
    {
        _logger.LogInformation("Received call to GET /products");
        
        var products = _productDbContext.Set<ProductEntity>()
            .Select(productEntity => _mapper.Map<Product>(productEntity))
            .ToList();
        
        return products.Count > 0 ? Ok(products) : NotFound("No products found.");
    }

    [HttpGet("{id:guid}")]
    public ActionResult<Product> GetProduct(Guid id)
    {
        _logger.LogInformation("Received call to GET /products/id");
        
        var productEntity = _productDbContext.Set<ProductEntity>()
            .Find(id);
        
        return productEntity != null ? Ok(_mapper.Map<Product>(productEntity)) : NotFound($"Product with id {id} not found.");
    }

    [HttpPost]
    [Authorize(Policy = nameof(TesterPolicy))]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] Product? newProduct)
    {
        _logger.LogInformation("Received call to POST /products");
        if (newProduct == null)
        {
            return BadRequest("Product cannot be null");
        }
        
        var product = _productDbContext.Set<ProductEntity>()
            .Add(_mapper.Map<ProductEntity>(newProduct))
            .Entity;
        
        await _productDbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, newProduct);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = nameof(UsernamePolicy))]
    public async Task<ActionResult> DeleteProduct(Guid id)
    {
        _logger.LogInformation("Received call to DELETE /products/id");

        var productToDelete = await _productDbContext.Set<ProductEntity>().FindAsync(id);
        if (productToDelete == null)
        {
            var message = $"Product with id {id} not found.";
            _logger.LogError(message);
            return NotFound(message);
        }

        _productDbContext.Set<ProductEntity>().Remove(productToDelete);
        await _productDbContext.SaveChangesAsync();

        return NoContent();
    }
}