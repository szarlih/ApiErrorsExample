using ApiErrorsExample.Models;
using ApiErrorsExample.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace ApiErrorsExample.Controllers;

[ApiController]
[Route("[controller]")]
public class FailedRequestsController : ControllerBase
{
    private readonly ILogger<FailedRequestsController> _logger;

    public FailedRequestsController(ILogger<FailedRequestsController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Get expected data when number is in range [200..299] and error for other ranges
    /// </summary>
    /// <param name="exampleNumber">The example number to determine the response</param>
    /// <returns>
    /// - 200-299: Expected data
    /// - 400-499: Corresponding client error
    /// - 444: Special JSON error message
    /// - Other: 500 Internal server error
    /// </returns>
    /// <response code="200">Returns the expected data when the number is in range [200..299]</response>
    /// <response code="400">Returns a client error when the number is in range [400..499]</response>
    /// <response code="444">Returns a special JSON error message</response>
    /// <response code="500">Returns an internal server error for numbers outside the specified ranges</response>
    [HttpGet("{exampleNumber}", Name = "GetResponseDataOrErrorWithData")]
    [ProducesResponseType(typeof(ExpectedResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorData), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Special444Error), 444)]
    [ProducesResponseType(typeof(ErrorData), StatusCodes.Status500InternalServerError)]
    public ActionResult Get([FromRoute] int exampleNumber)
    {
        if (exampleNumber.IsInRange(200, 299))
        {
            return Ok(new ExpectedResponse { Data = "Expected data" });
        }

        if (exampleNumber == 444)
        {
            return StatusCode(
                444,
                new {
                    errorData = new
                    {
                        message = "Special error with status code 444",
                        timestamp = "2025-03-25 19:49:23",
                        weekOfYear = "Week 13",
                        cleanCodePrinciple = "Code is clean if it can be understood easily – by everyone on the team."
                    }
                }
            );
        }

        if (exampleNumber.IsInRange(400, 499))
        {
            return StatusCode(exampleNumber, new ErrorData { Message = $"Client error with status code {exampleNumber}" });
        }

        return StatusCode(500, new ErrorData { Message = "Internal server error" });
    }
}
