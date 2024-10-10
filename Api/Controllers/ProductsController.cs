using Microsoft.AspNetCore.Mvc;
using Persistence;
using Persistence.Entity;

namespace Api.Controllers;

[ApiController]
[Route("products")]
public class ProductsController : ControllerBase
{

    private readonly ILogger<ProductsController> _logger;
    private readonly ApplicationDbContext _productDbContext;

    public ProductsController(ILogger<ProductsController> logger, ApplicationDbContext productDbContext)
    {
        _logger = logger;
        _productDbContext = productDbContext;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ProductEntity>> GetProducts()
    {
        _logger.LogInformation("Received call to GET /products");
        var products = _productDbContext.Set<ProductEntity>().ToList();
        return products.Count > 0 ? Ok(products) : NotFound("No products found.");
    }

    [HttpGet("{id:int}")]
    public ActionResult<ProductEntity> GetProduct(int id)
    {
        _logger.LogInformation("Received call to GET /products/id");
        var product = _productDbContext.Set<ProductEntity>().Find(id);
        return product != null ? Ok(product) : NotFound($"Product with id {id} not found.");
    }

    [HttpPost]
    public async Task<ActionResult<ProductEntity>> CreateProduct([FromBody] ProductEntity? newProduct)
    {
        _logger.LogInformation("Received call to POST /products");
        if (newProduct == null)
        {
            return BadRequest("Product cannot be null");
        }
        _productDbContext.Set<ProductEntity>().Add(newProduct);
        await _productDbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, newProduct);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
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