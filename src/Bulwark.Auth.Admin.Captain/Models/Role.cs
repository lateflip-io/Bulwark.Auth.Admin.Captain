using System.Text.Json.Serialization;

namespace Bulwark.Auth.Admin.Captain.Models;

public class Role
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("permissions")]
    public List<string> Permissions { get; set; }
    [JsonPropertyName("created")]
    public DateTime Created { get; set; }
    [JsonPropertyName("modified")]
    public DateTime Modified { get; set; }
    
    public Role()
    {
        Id = string.Empty;
        Permissions = new List<string>();
        Name = string.Empty;
    }
}