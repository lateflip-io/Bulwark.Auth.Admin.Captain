namespace Bulwark.Auth.Admin.Captain.Models;

public class MagicCode
{
    public string Id { get; set; }

    public string UserId { get; set; }
    public string Code { get; set; }

    public DateTime Created { get; set; }
    public DateTime Expires { get; set; }

    public MagicCode()
    {
        Id = string.Empty;
        UserId = string.Empty;
        Code = string.Empty;
    }
}
