using System.Net;
using AutoMapper;
using Domain.Dto;
using Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repository.Abstractions;
using Security.Policy;

namespace Api.Controllers;

[ApiController]
[Route("products")]
[Authorize]
public class ProductsController : ControllerBase
{

    private readonly ILogger<ProductsController> _logger;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductsController(ILogger<ProductsController> logger, IProductRepository productRepository, IMapper mapper)
    {
        _logger = logger;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Product>>> GetProducts()
    {
        _logger.LogInformation("Received call to GET /products");

        var products = (await _productRepository.GetAllAsync())
            .Select(productEntity => _mapper.Map<Product>(productEntity))
            .ToList();
        
        return products.Count > 0 ? Ok(products) : NotFound("No products found.");
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Product>> GetProduct(Guid id)
    {
        _logger.LogInformation("Received call to GET /products/id");

        var productEntity = await _productRepository.FindByIdAsync(id);
        
        return productEntity != null ? Ok(_mapper.Map<Product>(productEntity)) : NotFound($"Product with id {id} not found.");
    }

    [HttpPost]
    [Authorize(Policy = nameof(TesterPolicy))]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] Product? newProduct)
    {
        _logger.LogInformation("Received call to POST /products");
        if (newProduct is null)
        {
            return BadRequest("Product cannot be null");
        }

        var productId = await _productRepository.CreateAsync(_mapper.Map<ProductEntity>(newProduct));

        return CreatedAtAction(nameof(GetProduct), new { id = productId }, newProduct);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = nameof(UsernamePolicy))]
    public async Task<ActionResult> DeleteProduct(Guid id)
    {
        _logger.LogInformation("Received call to DELETE /products/id");

        var productToDelete = await _productRepository.FindByIdAsync(id);
        if (productToDelete is null)
        {
            var message = $"Product with id {id} not found.";
            _logger.LogError(message);
            return NotFound(message);
        }

        var result = await _productRepository.DeleteAsync(productToDelete);

        return result ? NoContent() : StatusCode((int)HttpStatusCode.InternalServerError);
    }
}