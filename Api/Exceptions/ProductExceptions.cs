namespace Api.Exceptions;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(Guid productId) : base($"Product with id {productId} was not found") { }
    
    public ProductNotFoundException(string name) : base($"Product with name {name} was not found") { }
};

public class ProductsNotFoundException() 
    : NotFoundException("No product found.");

public class ProductAlreadyExistsException(string productName) 
    : ConflictException($"Product with name {productName} is already exists");