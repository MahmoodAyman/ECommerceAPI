using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications;

public class ProductFilterSortPaginationSpecification : BaseSpecification<Product>
{
    public ProductFilterSortPaginationSpecification(ProductSpecificationParams specParams) : base(x =>
    (!specParams.Brands.Any() || specParams.Brands.Contains(x.Brand)) &&
    (!specParams.Types.Any() || specParams.Types.Contains(x.Type)))
    {
        ApplyPaging(specParams.PageSize * (specParams.pageNumber - 1), specParams.PageSize);
        
        switch (specParams.Sort)
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
