using System.Net;
using System.Net.Http.Json;
using Moq;
using Shared.Constant;
using Shared.Exceptions;
using Shared.Model;
using Xunit;

namespace Tests.Controller;

public partial class ProfileControllerTests
{
    [Fact]
    public async Task CreateProfile()
    {
        var url = Route.Request.CreateProfile;
        var response = await _httpClient.PostAsJsonAsync(url, User);
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public Task CreateProfile_AlreadyExists()
    {
        return AssertCreateProfileHandlesException(new AlreadyExistsException(Handle), HttpStatusCode.Conflict);
    }

    private async Task AssertCreateProfileHandlesException(Exception exception, HttpStatusCode status)
    {
        _mock.Setup(repository => repository.AddUser(It.IsAny<User>())).ThrowsAsync(exception);

        var url = Route.Request.CreateProfile;
        var response = await _httpClient.PostAsJsonAsync(url, User);
        Assert.Equal(status, response.StatusCode);
    }
}