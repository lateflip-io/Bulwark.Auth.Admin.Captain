namespace Bulwark.Auth.Admin.Captain.Models;
public class AuthToken
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string DeviceId { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    
    public AuthToken()
    {
        Id = String.Empty;
        UserId = string.Empty;
        DeviceId = string.Empty;
        AccessToken = string.Empty;
        RefreshToken = string.Empty;
    }
}