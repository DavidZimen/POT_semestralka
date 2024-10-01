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
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        _logger.LogInformation("Received call to GET /products");
        var products = await _productDbContext.Products.ToListAsync();
        return products.Count > 0 ? Ok(products) : NotFound("No products found.");
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        _logger.LogInformation("Received call to GET /products/id");
        var product = await _productDbContext.Products.FindAsync(id);
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
}