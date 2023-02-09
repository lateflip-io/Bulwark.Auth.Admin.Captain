using System.Text.Json.Serialization;

namespace Bulwark.Admin.Client.Models;

public class Permission
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("created")]
    public DateTime Created { get; set; }

    public Permission()
    {
        Id = String.Empty;
        Created = DateTime.Now;
    }
}