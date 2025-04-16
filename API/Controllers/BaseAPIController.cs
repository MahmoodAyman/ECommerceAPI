using System;
using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseAPIController : ControllerBase
{
    protected async Task<ActionResult> CreatePagedResults<T>(IGenericRepository<T> repository, ISpecification<T> spec, int pageNumber, int pageSize) where T : BaseEntity
    {
        var items = await repository.ListAsync(spec);
        var count = await repository.CountAsync(spec);
        var pagination = new Pagination<T>(pageNumber, pageSize, count, items);
        return Ok(pagination);

    }
}
