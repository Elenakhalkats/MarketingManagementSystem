using MarketingManagementSystem.Application.Features.ProductsSales.Commands;
using MarketingManagementSystem.Application.Features.ProductsSales.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MarketingManagementSystem.Web.Controllers;
[ApiController]
public class ProductsSalesController : Controller
{
	private readonly IMediator _mediator;
	public ProductsSalesController(IMediator mediator)
	{
		_mediator = mediator;
	}
    [HttpGet("GetProduct")]
    public async Task<IActionResult> GetProductById([FromQuery] int id)
    {
        var result = await _mediator.Send(new GetProductByIdQuery { Id = id });
        return Ok(result);
    }
    [HttpPost("AddProduct")]
    public async Task<IActionResult> AddProduct([FromBody] AddProductCommand req)
    {
        var result = await _mediator.Send(req);
        return Ok(result);
    }
    [HttpGet("GetSales")]
    public async Task<IActionResult> GetSales([FromQuery] GetSalesQuery? filterObjects)
    {
        var result = await _mediator.Send(filterObjects);
        return Ok(result);
    }

    [HttpPost("AddSale")]
    public async Task<IActionResult> AddSale([FromBody] AddSaleCommand req)
    {
        var result = await _mediator.Send(req);
        return Ok(result);
    }
}
