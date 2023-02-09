using Bulwark.Admin.Client.Exceptions;

namespace Bulwark.Captain.Tests;

public class RolesTest
{
    private readonly Admin.Client.Captain _captain;
    public RolesTest()
    {
        _captain = new Admin.Client.Captain("http://localhost:5086");
    }

    [Fact]
    public async void CreateAndRead()
    {
        var role = GenerateRole();

        try
        {
            await _captain.Roles.Create(role);
            var roles = await _captain.Roles.Read();
            Assert.Contains(roles, r => r.Name == role);
        }
        catch (BulwarkAdminException exception)
        {
            Assert.True(false, exception.Message);
        }
    }
    
    [Fact]
    public async void Update()
    {
        var name = GenerateRole();
        var newName = GenerateRole();

        try
        {
            await _captain.Roles.Create(name);
            var role = await _captain.Roles.Read(name);
            await _captain.Roles.Update(role.Id, newName);
            var roleUpdate = await _captain.Roles.Read(newName);
            Assert.Equal(newName, roleUpdate.Name);
            Assert.Equal(role.Id, roleUpdate.Id);
        }
        catch (BulwarkAdminException exception)
        {
            Assert.True(false, exception.Message);
        }
    }
    
    [Fact]
    public async void Delete()
    {
        var name = GenerateRole();

        try
        {
            await _captain.Roles.Create(name);
            var role = await _captain.Roles.Read(name);
            Assert.Equal(name, role.Name);
            await _captain.Roles.Delete(name);
            role = await _captain.Roles.Read(name);
        }
        catch (BulwarkAdminException exception)
        {
            Assert.Contains(name ,exception.Message);
        }
    }
    
    [Fact]
    public async void RoleAccountAddReadDelete()
    {
        var role = GenerateRole();
        var email = GenerateEmail();

        try
        {
            await _captain.Roles.Create(role);
            var roles = await _captain.Roles.Read();
            Assert.Contains(roles, r => r.Name == role);
            var roleObject = roles.First(r => r.Name == role);
            await _captain.Accounts.Create(email, true);
            var account = await _captain.Accounts.ReadByEmail(email);
            Assert.Equal(email, account.Email);
            await _captain.Roles.AddToAccount(account.Id, roleObject.Id);
            account = await _captain.Accounts.ReadByEmail(email);
            Assert.Contains(account.Roles, r => r == role);
            var accountRoles = await _captain.Roles.ReadByAccount(account.Id);
            Assert.Contains(accountRoles, r => r == role);
            await _captain.Roles.DeleteFromAccount(account.Id, roleObject.Id);
            account = await _captain.Accounts.ReadByEmail(email);
            Assert.DoesNotContain(account.Roles, r => r == role);
            accountRoles = await _captain.Roles.ReadByAccount(account.Id);
            Assert.DoesNotContain(accountRoles, r => r == role);
        }
        catch (BulwarkAdminException exception)
        {
            Assert.True(false, exception.Message);
        }
    }
    
    private string GenerateRole()
    {
        return $"role_{Guid.NewGuid()}";
    }
    
    private string GenerateEmail()
    {
        return $"email_{Guid.NewGuid()}@lateflip.io";
    }
}