using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Route("api/[controller]")]
[ApiController]
public class ProductsController(IGenericRepository<Product> repository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery] ProductSpecificationParams specParams)
    {
        var spec = new ProductFilterSortPaginationSpecification(specParams);
        var products = await repository.ListAsync(spec);
        var count = await repository.CountAsync(spec);
        var pagination = new Pagination<Product>(specParams.pageNumber, specParams.PageSize, count, products);
        return Ok(pagination);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct([FromRoute] int id)
    {
        var product = await repository.GetByIdAsync(id);

        if (product == null) return NotFound();

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
    {
        repository.Add(product);
        if (await repository.SaveAllAsync())
        {
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }
        return BadRequest("Something went wrong during creating a product");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct([FromRoute] int id, [FromBody] Product product)
    {
        if (product.Id != id || !ProductExists(id))
        {
            return BadRequest("Cannot update this product");
        }
        repository.Update(product);
        if (await repository.SaveAllAsync())
        {
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct([FromRoute] int id)
    {
        var product = await repository.GetByIdAsync(id);

        if (product == null) return NotFound();

        repository.Remove(product);

        if (await repository.SaveAllAsync())
        {
            return NoContent();
        }
        return BadRequest("Something went wrong");
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetProductBrands()
    {
        // TODO: Implement the method
        var spec = new BrandListSpecification();
        return Ok(await repository.ListAsync(spec));
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetProductTypes()
    {
        // TODO: Implement the method
        var spec = new TypeListSpecification();
        return Ok(await repository.ListAsync(spec));
    }

    private bool ProductExists(int id)
    {
        return repository.Exists(id);
    }
}