using MarketingManagementSystem.Web.Features.Distributors.Commands;
using MarketingManagementSystem.Web.Features.Distributors.Queries;
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
        var result = await _mediator.Send(new GetDistributorByIdQuery { Id = id });
        return Ok(result);
    }

    [HttpPost("AddDistributor")]
    public async Task<IActionResult> AddDistributor([FromBody] AddDistributorCommand req)
    {
        var result = await _mediator.Send(req);
        return Ok(result);
    }
    [HttpPut("UpdateDistributor")]
    public async Task<IActionResult> UpdateDistributorById([FromBody] UpdateDistributorCommand req)
    {
        var result = await _mediator.Send(req);
        if (result)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
    [HttpDelete("DeleteDistributor")]
    public async Task<IActionResult> DeleteDistributor([FromQuery] int id)
    {
        var result = await _mediator.Send(new DeleteDistributorCommand { Id = id });
        return Ok(result);
    }
    [HttpGet("GetDistributorRecomendations")]
    public async Task<IActionResult> GetRecommendationsById([FromQuery] int id)
    {
        var result = await _mediator.Send(new GetRecommendationsByDistributorIdQuery{ Id = id });
        return Ok(result);
    }
    [HttpPost("AddNewRecomendation")]
    public async Task<IActionResult> RecommendDistributor(
        [FromQuery] int recommendatorId,
        [FromQuery] int recommendToId)
    {
        var result = await _mediator.Send(new AddDistributorRecommendations(recommendatorId, recommendToId));
        return Ok(result);
    }
    [HttpPost("GetDistributorBonuses")]
    public async Task<IActionResult> GetBonusesByDistributorId([FromQuery] int id)
    {
        var result = await _mediator.Send(new GetBonusesByDistributorIdQuery { Id = id });
        return Ok(result);
    }

    [HttpPost("CountDistributorsBonuses")]
    public async Task<IActionResult> CountBonus([FromBody] CountBonusCommand req)
    {
        var result = await _mediator.Send(req);
        return Ok(result);
    }
    [HttpGet("GetDistributorWithMinBonus")]
    public async Task<IActionResult> GetDistributorWithMinBonus()
    {
        var result = await _mediator.Send(new GetDistributorWithMinBonusQuery());
        return Ok(result);
    }
    [HttpGet("GetDistributorWithMaxBonus")]
    public async Task<IActionResult> GetDistributorWithMaxBonus()
    {
        var result = await _mediator.Send(new GetDistributorWithMaxBonusQuery());
        return Ok(result);
    }
}