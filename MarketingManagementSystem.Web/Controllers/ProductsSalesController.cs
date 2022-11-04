using MarketingManagementSystem.Web.Features.ProductsSales.Commands;
using MarketingManagementSystem.Web.Features.ProductsSales.Queries;
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
        try
        {
            var result = await _mediator.Send(new GetProductByIdQuery { Id = id });
            return Ok(result);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpPost("AddProduct")]
    public async Task<IActionResult> AddProduct([FromBody] AddProductCommand req)
    {
        try
        {
            var result = await _mediator.Send(req);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        } 
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
        try
        {
            var result = await _mediator.Send(req);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
