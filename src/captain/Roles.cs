using System.Text.Json;
using Bulwark.Admin.Client.Exceptions;
using Bulwark.Admin.Client.Models;
using RestSharp;

namespace Bulwark.Admin.Client;

public class Roles
{
    private readonly RestClient _client;

    public Roles(string baseUri)
    {
        _client = new RestClient(baseUri);
        _client.AddDefaultHeader("Content-Type", "application/json");
        _client.AddDefaultHeader("Accept", "application/json");
    }

    public Roles(RestClient client)
    {
        _client = client;
    }

    public async Task Create(string name)
    {
        var request = new RestRequest("roles/create/{name}");
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

    public async Task<List<Role>> Read()
    {
        var request = new RestRequest("roles/read");
        var response = await _client.ExecuteGetAsync(request);
        if ((int)response.StatusCode == 200 && response.Content != null)
            return JsonSerializer.Deserialize<List<Role>>(response.Content) ??
                   new List<Role>();

        if (response.Content == null) return new List<Role>();
        var error = JsonSerializer
            .Deserialize<Error>(response.Content);
        if (error is { Detail: { } })
        {
            throw new BulwarkAdminException(error.Detail);
        }

        throw new BulwarkAdminException("Unknown error");
    }

    public async Task<Role> Read(string name)
    {
        var request = new RestRequest("roles/read/{name}");
        request.AddUrlSegment("name", name);
        var response = await _client.ExecuteGetAsync(request);
        if ((int)response.StatusCode == 200 && response.Content != null)
            return JsonSerializer.Deserialize<Role>(response.Content) ??
                   throw new BulwarkAdminException("No Role found");

        if (response.Content == null) throw new BulwarkAdminException("No content");
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
        var request = new RestRequest("roles/read/account/{accountId}");
        request.AddUrlSegment("accountId", accountId);

        var response = await _client.ExecuteGetAsync(request);
        if ((int)response.StatusCode == 200 && response.Content != null)
            return JsonSerializer.Deserialize<List<string>>(response.Content) ??
                   throw new BulwarkAdminException("No Roles found");

        if (response.Content == null) throw new BulwarkAdminException("No content");
        var error = JsonSerializer
            .Deserialize<Error>(response.Content);
        if (error is { Detail: { } })
        {
            throw new BulwarkAdminException(error.Detail);
        }

        throw new BulwarkAdminException("Unknown error");
    }

    public async Task Update(string id, string newName)
    {
        var request = new RestRequest("roles/update");
        request.AddJsonBody(new
        {
            Id = id,
            Name = newName
        });
        try
        {
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
        catch (Exception e)
        {
            throw new BulwarkAdminException(e.Message);
        }
    }
    
    public async Task AddToAccount(string accountId, string roleId)
    {
        var request = new RestRequest("roles/{roleId}/add/account/{accountId}");
        request.AddUrlSegment("roleId", roleId);
        request.AddUrlSegment("accountId", accountId);

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

    public async Task Delete(string name)
    {
        var request = new RestRequest("roles/delete/{name}");
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
    
    public async Task DeleteFromAccount(string accountId, string roleId)
    {
        var request = new RestRequest("roles/{roleId}/delete/account/{accountId}");
        request.AddUrlSegment("roleId", roleId);
        request.AddUrlSegment("accountId", accountId);

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
}