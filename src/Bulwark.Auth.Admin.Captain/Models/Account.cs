using System.Text.Json.Serialization;

namespace Bulwark.Auth.Admin.Captain.Models;

public class Account
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("isVerified")]
    public bool IsVerified { get; set; }
    [JsonPropertyName("isEnabled")]
    public bool IsEnabled { get; set; }
    [JsonPropertyName("isDeleted")]
    public bool IsDeleted { get; set; }
    [JsonPropertyName("socialProviders")]
    public List<SocialProvider> SocialProviders { get; set; }
    [JsonPropertyName("roles")]
    public List<string> Roles { get; set; }
    [JsonPropertyName("permissions")]
    public List<string> Permissions { get; set; }
    [JsonPropertyName("authTokens")]
    public List<AuthToken> AuthTokens { get; set; }
    [JsonPropertyName("magicCodes")]
    public List<MagicCode> MagicCodes { get; set; }
    [JsonPropertyName("created")]
    public DateTime Created { get; set; } 
    [JsonPropertyName("modified")]
    public DateTime Modified { get; set; }
    
    public Account()
    {
        Id = string.Empty;
        Email = string.Empty;
        Roles = new List<string>();
        Permissions = new List<string>();
        AuthTokens = new List<AuthToken>();
        MagicCodes = new List<MagicCode>();
        SocialProviders = new List<SocialProvider>();
    }
}

public class SocialProvider
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("socialId")]
    public string SocialId { get; set; }
    
    public SocialProvider()
    {
        Name = string.Empty;
        SocialId = string.Empty;
    }
}