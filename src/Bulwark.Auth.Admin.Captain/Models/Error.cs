using System.Text.Json.Serialization;

namespace Bulwark.Auth.Admin.Captain.Models;

public class Error
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    [JsonPropertyName("detail")]
    public string? Detail { get; set; }
    [JsonPropertyName("type")]
    public string? Type { get; set; }
    [JsonPropertyName("statusCode")]
    public string? StatusCode { get; set; }
               
    public Error(){}
}