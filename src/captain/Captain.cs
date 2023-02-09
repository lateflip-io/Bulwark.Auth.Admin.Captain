using System.Text.Json;
using Bulwark.Admin.Client.Exceptions;
using RestSharp;

namespace Bulwark.Admin.Client;

public class Captain
{
    private readonly RestClient _client;
    public Accounts Accounts { get; }
    public Permissions Permissions { get; }
   
    public Roles Roles { get; }

    public Captain(string baseUri)
    {
        _client = new RestClient(baseUri);
        _client.AddDefaultHeader("Content-Type", "application/json");
        _client.AddDefaultHeader("Accept", "application/json");
        Accounts = new Accounts(_client);
        Permissions = new Permissions(_client);
        Roles = new Roles(_client);
    }
    
    public async Task<bool> IsHealthy()
    {
        var request = new RestRequest("health");
        var response = await _client.ExecuteGetAsync(request);

        return (int)response.StatusCode == 200;
    }
}