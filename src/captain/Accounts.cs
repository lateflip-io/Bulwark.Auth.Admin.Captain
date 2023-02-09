using System.Text.Json;
using Bulwark.Admin.Client.Exceptions;
using Bulwark.Admin.Client.Models;
using RestSharp;

namespace Bulwark.Admin.Client;

public class Accounts
{
    private readonly RestClient _client;
    public Accounts(string baseUri)
    {
        _client = new RestClient(baseUri);
        _client.AddDefaultHeader("Content-Type", "application/json");
        _client.AddDefaultHeader("Accept", "application/json");
    }
    
    public Accounts(RestClient client)
    {
        _client = client;
    }
    
    public async Task Create(string email, bool isVerified)
    {
        var payload = new
        {
            Email = email,
            IsVerified = isVerified
        };
        
        var request = new RestRequest("accounts/create")
            .AddJsonBody(payload);
        
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
   public async Task<List<Account>> Read(int perPage, int page, string sortField)
    {
        var request = new RestRequest("accounts/read");
        request.AddParameter("perPage", perPage);
        request.AddParameter("page", page);
        request.AddParameter("sortField", sortField);
      
        var response = await _client.ExecuteGetAsync(request);
        if ((int)response.StatusCode == 200 && response.Content != null)
            return JsonSerializer.Deserialize<List<Account>>(response.Content) ?? 
                   throw new BulwarkAdminException("No content");

        if (response.Content == null) throw new BulwarkAdminException("No content");
        var error = JsonSerializer
            .Deserialize<Error>(response.Content);
        if (error is { Detail: { } })
        {
            throw new BulwarkAdminException(error.Detail);
        }

        throw new BulwarkAdminException("Unknown error");
    }
   
   public async Task<Account> ReadByEmail(string email)
   {
       var request = new RestRequest("accounts/read/{email}");
       request.AddUrlSegment("email", email);
       
       var response = await _client.ExecuteGetAsync(request);
       if ((int)response.StatusCode == 200 && response.Content != null)
           return JsonSerializer.Deserialize<Account>(response.Content) ?? 
                  throw new BulwarkAdminException("No account found");

       if (response.Content == null) throw new BulwarkAdminException("No content");
       var error = JsonSerializer
           .Deserialize<Error>(response.Content);
       if (error is { Detail: { } })
       {
           throw new BulwarkAdminException(error.Detail);
       }

       throw new BulwarkAdminException("Unknown error");
   }
   
   public async Task<Account> ReadById(string id)
   {
       var request = new RestRequest("accounts/read/id/{id}");
       request.AddUrlSegment("id", id);
       
       var response = await _client.ExecuteGetAsync(request);
       if ((int)response.StatusCode == 200 && response.Content != null)
           return JsonSerializer.Deserialize<Account>(response.Content) ?? 
                  throw new BulwarkAdminException("No account found");

       if (response.Content == null) throw new BulwarkAdminException("No content");
       var error = JsonSerializer
           .Deserialize<Error>(response.Content);
       if (error is { Detail: { } })
       {
           throw new BulwarkAdminException(error.Detail);
       }

       throw new BulwarkAdminException("Unknown error");
   }
   
   public async Task SoftDelete(string email)
    {
        var request = new RestRequest($"accounts/delete/{email}");
        var response = await _client.ExecutePutAsync(request);

        if ((int)response.StatusCode >= 400)
        {
            if (response.Content != null)
            {
                var error = JsonSerializer
                    .Deserialize<Error>(response.Content);
                if (error is { Detail: { } })
                {
                    throw new BulwarkAdminException(error.Detail);
                }
                
                throw new BulwarkAdminException("Unknown error");
            }
        }
    }
   
   public async Task HardDelete(string email)
   {
       var request = new RestRequest($"accounts/delete/{email}");
       var response = await _client.DeleteAsync(request);

       if ((int)response.StatusCode >= 400)
       {
           if (response.Content != null)
           {
               var error = JsonSerializer
                   .Deserialize<Error>(response.Content);
               if (error is { Detail: { } })
               {
                   throw new BulwarkAdminException(error.Detail);
               }
                
               throw new BulwarkAdminException("Unknown error");
           }
       }
   }
   
   public async Task Disable(string email)
   {
       var request = new RestRequest($"accounts/disable/{email}");
       request.AddUrlSegment("email",email);
       
       var response = await _client.ExecutePutAsync(request);

       if ((int)response.StatusCode >= 400)
       {
           if (response.Content != null)
           {
               var error = JsonSerializer
                   .Deserialize<Error>(response.Content);
               if (error is { Detail: { } })
               {
                   throw new BulwarkAdminException(error.Detail);
               }
                
               throw new BulwarkAdminException("Unknown error");
           }
       }
   }
   
   public async Task Enable(string email)
   {
       var request = new RestRequest($"accounts/enable/{email}");
       request.AddUrlSegment("email",email);
       
       var response = await _client.ExecutePutAsync(request);

       if ((int)response.StatusCode >= 400)
       {
           if (response.Content != null)
           {
               var error = JsonSerializer
                   .Deserialize<Error>(response.Content);
               if (error is { Detail: { } })
               {
                   throw new BulwarkAdminException(error.Detail);
               }
                
               throw new BulwarkAdminException("Unknown error");
           }
       }
   }
   public async Task ChangeEmail(string email, string newEmail)
   {
       var payload = new
       {
           Email = email,
           NewEmail = newEmail
       };
        
       var request = new RestRequest("accounts/update/email")
           .AddJsonBody(payload);
        
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
}