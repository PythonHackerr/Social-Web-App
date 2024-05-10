using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using Shared.Exceptions;
using Shared.Model;
using Tests.Helpers;
using UserProfileService.Repository;
using Xunit;

namespace Tests.Repository;

public class UserRepositoryTests : IAsyncLifetime
{
    private IUserRepository Repo { get; set; }

    private string Handle { get; set; }
    private string Sub { get; set; }
    private User User { get; set; }
    
    public async Task InitializeAsync()
    {
        Repo = new MySqlUserRepository(DbHelper.Connection(), DbHelper.ConnectionString);

        var random = new Random();

        Handle = Guid.NewGuid().ToString();
        Sub = Guid.NewGuid().ToString();
        
        User = new User
        {
            Handle = Handle,
            DisplayName = Guid.NewGuid().ToString(),
            Email = Guid.NewGuid().ToString(),
            FollowerCount = random.Next(),
            FollowingCount = random.Next(),
            JoinDate = DateTime.Today,
            Sub = Sub
        };
    }

    public async Task DisposeAsync()
    {
        try
        {
            await Repo.DeleteUser(Handle);
        }
        catch (NotFoundException)
        {
            // nothing to clean up
        }
    }

    [Fact]
    public async Task AddProfile()
    {
        await Repo.AddUser(User);
        var storedProfile = await Repo.GetUser(Handle);
        
        Assert.True(storedProfile?.Id >= 0);
        
        storedProfile.Id = 0;
        Assert.Equal(User, storedProfile);
    }

    [Fact]
    public async Task AddProfile_AlreadyExists()
    {
        await Repo.AddUser(User);
        await Assert.ThrowsAsync<AlreadyExistsException>(() => Repo.AddUser(User));
    }

    [Fact]
    public async Task GetProfile_NotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(() => Repo.GetUser(Handle));
    }

    [Fact]
    public async Task DeleteProfile()
    {
        await Repo.AddUser(User);
        await Repo.DeleteUser(Handle);
        await GetProfile_NotFound();
    }

    [Fact]
    public async Task DeleteProfile_NotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(() => Repo.DeleteUser(Handle));
    }
    
    [Fact]
    public async Task HasProfile_Handle()
    {
        await Repo.AddUser(User);
        var exists = await Repo.HasProfileWithHandle(Handle);
        Assert.True(exists);
        await Repo.DeleteUser(Handle);
        var notExists = await Repo.HasProfileWithHandle(Handle);
        Assert.False(notExists);
    }
    
    [Fact]
    public async Task HasProfile_Sub()
    {
        await Repo.AddUser(User);
        var exists = await Repo.HasProfileWithSub(Sub);
        Assert.True(exists);
        await Repo.DeleteUser(Handle);
        var notExists = await Repo.HasProfileWithSub(Sub);
        Assert.False(notExists);
    }
}