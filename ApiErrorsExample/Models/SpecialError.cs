namespace ApiErrorsExample.Models;

/// <summary>
/// Special error response for 444 status code
/// </summary>
public class Special444Error
{
    /// <summary>
    /// Error data containing detailed information
    /// </summary>
    public ErrorDetails errorData { get; set; }
}

/// <summary>
/// Detailed error information
/// </summary>
public class ErrorDetails
{
    /// <summary>
    /// Error message
    /// </summary>
    public string message { get; set; }
    
    /// <summary>
    /// Timestamp when the error occurred
    /// </summary>
    public string timestamp { get; set; }
    
    /// <summary>
    /// Current week of the year
    /// </summary>
    public string weekOfYear { get; set; }
    
    /// <summary>
    /// Clean code principle
    /// </summary>
    public string cleanCodePrinciple { get; set; }
}
