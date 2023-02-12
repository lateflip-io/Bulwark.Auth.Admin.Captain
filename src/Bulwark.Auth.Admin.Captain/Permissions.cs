using System.Text.Json;
using Bulwark.Auth.Admin.Captain.Exceptions;
using Bulwark.Auth.Admin.Captain.Models;
using RestSharp;

namespace Bulwark.Auth.Admin.Captain;

public class Permissions
{
    private readonly RestClient _client;
    
    public Permissions(string baseUri)
    {
        _client = new RestClient(baseUri);
        _client.AddDefaultHeader("Content-Type", "application/json");
        _client.AddDefaultHeader("Accept", "application/json");
    }
    
    public Permissions(RestClient client)
    {
        _client = client;
    }
    
    public async Task Create(string name)
    {
        var request = new RestRequest("permissions/create/{name}");
        request.AddUrlSegment("name", name);
        var response = await _client.ExecutePostAsync(request);
        if ((int)response.StatusCode < 400) return;
        if (response.Content == null) return;
        var error = JsonSerializer
            .Deserialize<Error>(response.Content);
        if (error is { Detail: { } })
        {
            throw new BulwarkAdminException(error.Detail);
        }
    }

    public async Task<List<Permission>> Read()
    {
        var request = new RestRequest("permissions/read");
        var response = await _client.ExecuteGetAsync(request);
        if ((int)response.StatusCode == 200 && response.Content != null)
            return JsonSerializer.Deserialize<List<Permission>>(response.Content) ?? 
                   new List<Permission>();

        if (response.Content == null) return new List<Permission>();
        var error = JsonSerializer
            .Deserialize<Error>(response.Content);
        if (error is { Detail: { } })
        {
            throw new BulwarkAdminException(error.Detail);
        }

        throw new BulwarkAdminException("Unknown error");
    }
    
    public async Task Delete(string name)
    {
        var request = new RestRequest("permissions/delete/{name}");
        request.AddUrlSegment("name", name);
        var response = await _client.DeleteAsync(request);
        if ((int)response.StatusCode < 400) return;
        if (response.Content == null) return;
        var error = JsonSerializer
            .Deserialize<Error>(response.Content);
        if (error is { Detail: { } })
        {
            throw new BulwarkAdminException(error.Detail);
        }
    }
    
    public async Task AddRole(string permission, string roleId)
    {
        var request = new RestRequest("permissions/{permission}/add/role/{roleId}");
        request.AddUrlSegment("permission", permission);
        request.AddUrlSegment("roleId", roleId);
        var response = await _client.ExecutePutAsync(request);
        if ((int)response.StatusCode < 400) return;
        if (response.Content == null) return;
        var error = JsonSerializer
            .Deserialize<Error>(response.Content);
        if (error is { Detail: { } })
        {
            throw new BulwarkAdminException(error.Detail);
        }
    }
    
    public async Task DeleteRole(string permission, string roleId)
    {
        var request = new RestRequest("permissions/{permission}/delete/role/{roleId}");
        request.AddUrlSegment("permission", permission);
        request.AddUrlSegment("roleId", roleId);
        var response = await _client.DeleteAsync(request);
        if ((int)response.StatusCode < 400) return;
        if (response.Content == null) return;
        var error = JsonSerializer
            .Deserialize<Error>(response.Content);
        if (error is { Detail: { } })
        {
            throw new BulwarkAdminException(error.Detail);
        }
    }
    
    public async Task<List<string>> ReadByRole(string roleId)
    {
        var request = new RestRequest("permissions/read/role/{roleId}");
        request.AddUrlSegment("roleId", roleId);
        var response = await _client.ExecuteGetAsync(request);
        if ((int)response.StatusCode == 200 && response.Content != null)
            return JsonSerializer.Deserialize<List<string>>(response.Content) ?? 
                   new List<string>();

        if (response.Content == null) return new List<string>();
        var error = JsonSerializer
            .Deserialize<Error>(response.Content);
        if (error is { Detail: { } })
        {
            throw new BulwarkAdminException(error.Detail);
        }

        throw new BulwarkAdminException("Unknown error");
    }

    public async Task<List<string>> ReadByAccount(string accountId)
    {
        var request = new RestRequest("permissions/read/account/{accountId}");
        request.AddUrlSegment("accountId", accountId);
        var response = await _client.ExecuteGetAsync(request);
        if ((int)response.StatusCode == 200 && response.Content != null)
            return JsonSerializer.Deserialize<List<string>>(response.Content) ?? 
                   new List<string>();

        if (response.Content == null) return new List<string>();
        var error = JsonSerializer
            .Deserialize<Error>(response.Content);
        if (error is { Detail: { } })
        {
            throw new BulwarkAdminException(error.Detail);
        }

        throw new BulwarkAdminException("Unknown error");
    }
}