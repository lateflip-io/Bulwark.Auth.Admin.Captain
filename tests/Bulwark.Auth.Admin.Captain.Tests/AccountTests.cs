namespace Bulwark.Auth.Admin.Captain.Tests;

public class AccountTests
{
    private readonly Captain _captain;
    public AccountTests()
    {
        //http://localhost:5086
        _captain = new Captain("http://localhost:8080");
    }
    
    [Fact]
    public async void Health()
    {
        Assert.True(await _captain.IsHealthy());
    }

    [Fact]
    public async void Create()
    {
        var email = CreateEmail();
        
        try{
            await _captain.Accounts.Create(email, true);
            var account = await _captain.Accounts.ReadByEmail(email);
            Assert.NotNull(account);
            Assert.True(account.Email == email);
            Assert.True(account.IsEnabled);
            Assert.True(account.IsVerified);
            Assert.False(account.IsDeleted);
            var accountById = await _captain.Accounts.ReadById(account.Id);
            Assert.NotNull(accountById);
            Assert.True(accountById.Email == email);
            Assert.True(accountById.IsEnabled);
            Assert.True(accountById.IsVerified);
            Assert.False(accountById.IsDeleted);
        }
        catch(Exception ex)
        {
            Assert.True(false, ex.Message);
        }
    }
    
    [Fact]
    public async void SoftDeleteAccount()
    {
        var email = CreateEmail();
        
        try{
            await _captain.Accounts.Create(email, true);
            await _captain.Accounts.SoftDelete(email);
            var account = await _captain.Accounts.ReadByEmail(email);
            Assert.NotNull(account);
            Assert.True(account.IsDeleted);
        }
        catch(Exception ex)
        {
            Assert.True(false, ex.Message);
        }
    }
    
    [Fact]
    public async void Disable()
    {
        var email = CreateEmail();
        
        try{
            await _captain.Accounts.Create(email, true);
            await _captain.Accounts.Disable(email);
            var account = await _captain.Accounts.ReadByEmail(email);
            Assert.NotNull(account);
            Assert.False(account.IsEnabled);
        }
        catch(Exception ex)
        {
            Assert.True(false, ex.Message);
        }
    }
    
    [Fact]
    public async void Enable()
    {
        var email = CreateEmail();
        
        try{
            await _captain.Accounts.Create(email, false);
            var account = await _captain.Accounts.ReadByEmail(email);
            Assert.NotNull(account);
            Assert.False(account.IsEnabled);
            await _captain.Accounts.Enable(email);
            account = await _captain.Accounts.ReadByEmail(email);
            Assert.NotNull(account);
            Assert.True(account.IsEnabled);
        }
        catch(Exception ex)
        {
            Assert.True(false, ex.Message);
        }
    }
    
    [Fact]
    public async void HardDeleteAccount()
    {
        var email = CreateEmail();
        
        try{
            await _captain.Accounts.Create(email, true);
            await _captain.Accounts.HardDelete(email);
            var account = await _captain.Accounts.ReadByEmail(email);
            Assert.Fail("Should not be able to read a deleted account");
        }
        catch(Exception ex)
        {
            Assert.Contains(email, ex.Message);
        }
    }
    
    [Fact]
    public async void ReadAccounts()
    {
        var email1 = CreateEmail();
        var email2 = CreateEmail();
        var email3 = CreateEmail();
        
        try{
            await _captain.Accounts.Create(email1, true);
            await _captain.Accounts.Create(email2, true);
            await _captain.Accounts.Create(email3, true);
            var accounts = await _captain.Accounts.Read(20,1, "email");
            Assert.NotNull(accounts);
            Assert.True(accounts.Count() >= 3);
        }
        catch(Exception ex)
        {
            Assert.True(false, ex.Message);
        }
    }
    
    [Fact]
    public async void ChangeEmail()
    {
        var email = CreateEmail();
        var newEmail = CreateEmail();
        
        try{
            await _captain.Accounts.Create(email, true);
            var account = await _captain.Accounts.ReadByEmail(email);
            Assert.NotNull(account);
            await _captain.Accounts.ChangeEmail(email, newEmail);
            account = await _captain.Accounts.ReadByEmail(newEmail);
            Assert.NotNull(account);
            Assert.True(account.Email == newEmail);
        }
        catch(Exception ex)
        {
            Assert.True(false, ex.Message);
        }
    }
    
    
    private static string CreateEmail()
    {
        return $"test_{Guid.NewGuid()}@lateflip.io";
    }
}