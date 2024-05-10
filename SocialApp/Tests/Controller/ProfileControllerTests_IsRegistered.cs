using System.Net;
using Shared.Constant;
using Xunit;

namespace Tests.Controller;

public partial class ProfileControllerTests
{
    [Fact]
    public async Task IsRegistered_Handle()
    {
        var url = $"{Route.Request.HasProfileWithHandle}{Handle}";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task IsRegistered_Sub()
    {
        var url = $"{Route.Request.HasProfileWithSub}{Sub}";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}