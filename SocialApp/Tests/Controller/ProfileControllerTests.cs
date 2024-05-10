using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Shared.Model;
using Tests.Extensions;
using UserProfileService.Repository;
using Xunit;

namespace Tests.Controller;

/// <summary>
/// These tests are here to make sure that the Controller emits correct responses
/// when wild stuff happens in the Repository (e.g. a user is not found)
/// </summary>
public partial class ProfileControllerTests : IAsyncLifetime
{
    private readonly Mock<IUserRepository> _mock = new();

    private HttpClient _httpClient = null!;

    private const string Handle = "foo";
    private const string Sub = "dchvdhhhhdsi";
    
    private User User = new()
    {
        Handle = Handle,
        DisplayName = "Test User",
        Email = "test@user.tk",
        FollowerCount = 5,
        FollowingCount = 9,
        Id = 123,
        JoinDate = DateTime.Now,
        Sub = Sub
    };

    public async Task InitializeAsync()
    {
        var application = new WebApplicationFactory<UserProfileService.Program>()
            .WithWebHostBuilder(builder => builder
                .ConfigureServices(collection => collection
                    .ReplaceWithSingleton(_mock.Object)));

        _httpClient = application.CreateClient();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}