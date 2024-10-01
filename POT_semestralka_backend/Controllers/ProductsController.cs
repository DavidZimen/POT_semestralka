using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POT_semestralka_backend.Database;
using POT_semestralka_backend.Models;

namespace POT_semestralka_backend.Controllers;

[ApiController]
[Route("products")]
public class ProductsController : ControllerBase
{

    private readonly ILogger<ProductsController> _logger;
    private readonly ProductDbContext _productDbContext;

    public ProductsController(ILogger<ProductsController> logger, ProductDbContext productDbContext)
    {
        _logger = logger;
        _productDbContext = productDbContext;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetProducts()
    {
        _logger.LogInformation("Received call to GET /products");
        var products = _productDbContext.Products.ToList();
        return products.Count > 0 ? Ok(products) : NotFound("No products found.");
    }

    [HttpGet("{id:int}")]
    public ActionResult<Product> GetProduct(int id)
    {
        _logger.LogInformation("Received call to GET /products/id");
        var product = _productDbContext.Products.Find(id);
        return product != null ? Ok(product) : NotFound($"Product with id {id} not found.");
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] Product? newProduct)
    {
        _logger.LogInformation("Received call to POST /products");
        if (newProduct == null)
        {
            return BadRequest("Product cannot be null");
        }
        _productDbContext.Products.Add(newProduct);
        await _productDbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProduct), new { id = newProduct.ProductId }, newProduct);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        _logger.LogInformation("Received call to DELETE /products/id");

        var productToDelete = await _productDbContext.Products.FindAsync(id);
        if (productToDelete == null)
        {
            var message = $"Product with id {id} not found.";
            _logger.LogError(message);
            return NotFound(message);
        }

        _productDbContext.Products.Remove(productToDelete);
        await _productDbContext.SaveChangesAsync();

        return NoContent();
    }
}