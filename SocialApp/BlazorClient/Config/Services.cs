namespace BlazorClient.Config;

/// <summary>
/// Afaik, Blazor WebAssembly can't handle environment variables.
/// </summary>
public class Services
{
    public string Timeline { get; set; } = string.Empty;
    public string Profile { get; set; } = string.Empty;
    public string Follow { get; set; } = string.Empty;
}