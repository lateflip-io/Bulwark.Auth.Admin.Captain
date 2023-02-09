using Bulwark.Admin.Client.Exceptions;

namespace Bulwark.Captain.Tests;

public class PermissionsTests
{
    private readonly Admin.Client.Captain _captain;
    
    public PermissionsTests()
    {
        //https://localhost:44332
        _captain = new Admin.Client.Captain("http://localhost:5086");
    }
    
    [Fact]
    public async void CreateReadDeletePermission()
    {
        var permission = GeneratePermission();

        try
        {
            await _captain.Permissions.Create(permission);
            var permissions = await _captain.Permissions.Read();
            Assert.Contains(permissions, p => p.Id == permission);
            await _captain.Permissions.Delete(permission);
            permissions = await _captain.Permissions.Read();
            Assert.DoesNotContain(permissions, p => p.Id == permission);
        }
        catch (BulwarkAdminException exception)
        {
            Assert.True(false, exception.Message);
        }
    }
    
    [Fact]
    public async void PermissionAddReadDeleteFromRole()
    {
        var permission = GeneratePermission();
        var role = GenerateRole();

        try
        {
            await _captain.Permissions.Create(permission);
            var permissions = await _captain.Permissions.Read();
            Assert.Contains(permissions, p => p.Id == permission);
            await _captain.Roles.Create(role);
            var roleObject = await _captain.Roles.Read(role);
            await _captain.Permissions.AddRole(permission, roleObject.Id);
            var perms = await _captain.Permissions.ReadByRole(roleObject.Id);
            Assert.Contains(perms, p => p == permission);
            await _captain.Permissions.DeleteRole(permission, roleObject.Id);
            perms = await _captain.Permissions.ReadByRole(roleObject.Id);
            Assert.DoesNotContain(perms, p => p == permission);
        }
        catch (BulwarkAdminException exception)
        {
            Assert.True(false, exception.Message);
        }
    }
    
    [Fact]
    public async void PermissionReadFromAccount()
    {
        var permission = GeneratePermission();
        var role = GenerateRole();

        try
        {
            await _captain.Permissions.Create(permission);
            var permissions = await _captain.Permissions.Read();
            Assert.Contains(permissions, p => p.Id == permission);
            await _captain.Roles.Create(role);
            var roleObject = await _captain.Roles.Read(role);
            await _captain.Permissions.AddRole(permission, roleObject.Id);
            var perms = await _captain.Permissions.ReadByRole(roleObject.Id);
            Assert.Contains(perms, p => p == permission);
            var account = GenerateAccount();
            await _captain.Accounts.Create(account, true);
            var accountObject = await _captain.Accounts.ReadByEmail(account);
            await _captain.Roles.AddToAccount(accountObject.Id, roleObject.Id);
            perms = await _captain.Permissions.ReadByAccount(accountObject.Id);
            Assert.Contains(perms, p => p == permission);
        }
        catch (BulwarkAdminException exception)
        {
            Assert.True(false, exception.Message);
        }
    }
    private static string GeneratePermission()
    {
        return $"permission_{Guid.NewGuid()}";
    }
    private static string GenerateRole()
    {
        return $"role_{Guid.NewGuid()}";
    }
    
    private static string GenerateAccount()
    {
        return $"account_{Guid.NewGuid()}@lateflip.io";
    }
}