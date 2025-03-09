using System;
using Core.Entities;

namespace Core.Specifications;

public class ProductSpecification: BaseSpecification<Product>
{
  public ProductSpecification(string? brand, string? type, string? sort) : base(x =>  // use base constructor to create a primary constructor
    (string.IsNullOrWhiteSpace(brand) || x.Brand == brand) &&
    (string.IsNullOrWhiteSpace(type) || x.Type == type)
  )
  {
    switch (sort)
    {
      case "priceAsc":
        AddOrderBy(x => x.Price);
        break;
      case "priceDesc":
        AddOrderByDescending(x => x.Price);
        break;
      default:
        AddOrderBy(x => x.Name);
        break;
    }

  }
}
