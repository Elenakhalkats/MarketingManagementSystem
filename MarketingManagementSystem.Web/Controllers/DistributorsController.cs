using MarketingManagementSystem.Core.Enums;
using MarketingManagementSystem.Core.Features.Distributors.Commands;
using MarketingManagementSystem.Core.Features.Distributors.Queries;
using MarketingManagementSystem.Web.Features.Distributors.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MarketingManagementSystem.Web.Controllers;
[ApiController]
public class DistributorsController : ControllerBase
{
    private readonly IMediator _mediator;
    public DistributorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetDistributors")]
    public async Task<IActionResult> GetDistributors([FromQuery] GetDistributorsQuery? filterObjects)
    {
        var result = await _mediator.Send(filterObjects);
        return Ok(result);
    }
    [HttpGet("GetDistributor")]
    public async Task<IActionResult> GetDistributorById([FromQuery] int id)
    {
        try
        {
            var result = await _mediator.Send(new GetDistributorByIdQuery { Id = id });
            return Ok(result);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("AddDistributor")]
    public async Task<IActionResult> AddDistributor([FromBody] AddDistributorCommand req)
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
    [HttpPut("UpdateDistributor")]
    public async Task<IActionResult> UpdateDistributorById([FromBody] UpdateDistributorCommand req)
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
    [HttpDelete("DeleteDistributor")]
    public async Task<IActionResult> DeleteDistributor([FromQuery] int id)
    {
        try
        {
            var result = await _mediator.Send(new DeleteDistributorCommand { Id = id });
            return Ok(result);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpGet("GetDistributorRecomendations")]
    public async Task<IActionResult> GetRecommendationsById([FromQuery] int id)
    {
        try
        {
            var result = await _mediator.Send(new GetRecommendationsByDistributorIdQuery { Id = id });
            return Ok(result);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpPost("AddNewRecomendation")]
    public async Task<IActionResult> RecommendDistributor(
        [FromQuery] int recommendatorId,
        [FromQuery] int recommendToId)
    {
        try
        {
            var result = await _mediator.Send(new AddDistributorRecommendations(recommendatorId, recommendToId));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost("GetDistributorBonuses")]
    public async Task<IActionResult> GetBonusesByDistributorId([FromQuery] int id)
    {
        try
        {
            var result = await _mediator.Send(new GetBonusesByDistributorIdQuery { Id = id });
            return Ok(result);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("CountDistributorsBonuses")]
    public async Task<IActionResult> CountBonus([FromBody] CountBonusCommand req)
    {
        var result = await _mediator.Send(req);
        return Ok(result);
    }
    [HttpGet("MinBonus")]
    public async Task<IActionResult> GetDistributorWithMinBonus()
    {
        var result = await _mediator.Send(new GetDistributorWithMinBonusQuery());
        return Ok(result);
    }
    [HttpGet("MaxBonus")]
    public async Task<IActionResult> GetDistributorWithMaxBonus()
    {
        var result = await _mediator.Send(new GetDistributorWithMaxBonusQuery());
        return Ok(result);
    }

}