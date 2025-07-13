using Blockbuster.Application.Movies.Queries;
using Blockbuster.Application.Movies.TransferObjects;
using Blockbuster.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Net;

namespace Blockbuster.API.Controllers;


[ApiController]
[Route("api/[controller]")]
[EnableRateLimiting("Sliding")]
public class BlockbusterController : ControllerBase
{
    
    private readonly IMediator _mediator;
    private readonly ILogger _logger;
    
    public BlockbusterController(IMediator mediator, ILogger<BlockbusterController> _logger)
    {
        _mediator = mediator;
        _logger = _logger ?? throw new ArgumentNullException(nameof(_logger));
    }

    [HttpGet]
    [Route("movies")]
    public async Task<ActionResult<MovieResponse>> GetMoviesAsync()
    {

        try
        {
            
            var result = await _mediator.Send(new GetMoviesQuery());

            return Ok(result);

        }
        catch (Exception ex) {

            _logger.LogError(ex, "Error while fetching movies: {Message}", ex.Message);
            return StatusCode((int)HttpStatusCode.InternalServerError, new { Error = "Failed Movie Retrieval" });
        
        }

    }

    [HttpGet]
    [Route("movie/{id}")]
    public async Task<ActionResult<MovieInfo>> GetMovieByIdAsync(string id)
    {

        try
        {

            var result = await _mediator.Send(new GetMovieInfoQuery() { Id = id});

            if (!string.IsNullOrEmpty(result.ID))
                return Ok(result);

            return NotFound();

        }
        catch (Exception ex)
        {

            _logger.LogError(ex, "Error while fetching movies: {Message}", ex.Message);
            return StatusCode((int)HttpStatusCode.InternalServerError, new { Error = "Failed Movie Retrieval" });

        }

    }
}
