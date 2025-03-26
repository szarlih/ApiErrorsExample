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
    /// Get expected data when number is in range [200..299]
    /// and error for other ranges
    /// </summary>
    /// <param name="exampleNumber">if in range [200...299] expected data,
    /// if in range [400...499] corresponding errors,
    /// - special error 444 json error message
    /// if in other range 500 error</param>
    /// <returns></returns>
    [HttpGet("{exampleNumber}", Name = "GetResponseDataOrErrorWithData")]
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
