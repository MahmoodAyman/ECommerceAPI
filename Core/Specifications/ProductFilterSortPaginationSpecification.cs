using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications;

public class ProductFilterSortPaginationSpecification : BaseSpecification<Product>
{
    public ProductFilterSortPaginationSpecification(string? brand, string? type, string? sort) : base(x =>
    (string.IsNullOrWhiteSpace(brand) || x.Brand.Contains(brand)) &&
    (string.IsNullOrWhiteSpace(type) || x.Type.Contains(type)))
    {
        switch (sort)
        {
            case "priceAsc":
                AddOrderBy(x => x.Price);
                break;
            case "priceDesc":
                AddOrderByDescending(x => x.Price);
                break;
            case "nameDesc":
                AddOrderByDescending(x => x.Name);
                break;
            default:
                AddOrderBy(x => x.Name);
                break;
        }
    }
}
