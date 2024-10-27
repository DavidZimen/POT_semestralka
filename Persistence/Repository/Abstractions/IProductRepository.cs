﻿using Domain.Entity;

namespace Persistence.Repository.Abstractions;

public interface IProductRepository : IBaseRepository<ProductEntity, Guid>
{
    Task<ProductEntity?> FindProductByNameAsync(string name);
}