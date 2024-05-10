using Newtonsoft.Json;
using Shared.Dtos;
using Shared.Mapping;
using Shared.Model;
using UserTimelineService.Repository;

namespace UserTimelineService.Tests;

public class EndpointTest
{
    private readonly string hostUrl;
    private readonly uint port;
    
    private readonly HttpClient httpClient;
    private readonly IPostRepository repository;

    public EndpointTest(string hostUrl, uint port, IPostRepository repository)
    {
        this.repository = repository;
        this.hostUrl = hostUrl;
        this.port = port;
        
        httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }

    public string TestGetTweets()
    {
        const string handle = "testuser";

        var fromRepository = repository.GetPosts(handle).Result;
        var fromEndpoint = GetTweets(handle);

        return TestUtility.EqualCount("GetTweets", fromRepository, fromEndpoint);
    }

    private IEnumerable<Post> GetTweets(string handle)
    {
        var task = Call(HttpMethod.Get, $"https://{hostUrl}:{port}/Timeline/GetTweets?user={handle}");
        var result = task.Result;
        
        return DeserializeTweets(result);
    }
    
    private IEnumerable<Post> DeserializeTweets(string json)
    {
        var tweets = JsonConvert.DeserializeObject<PostDto[]>(json) ?? Enumerable.Empty<PostDto>();
        
        return tweets.Select(dto => dto.ToModel());
    }
    
    private async Task<string> Call(HttpMethod httpMethod, string uri)
    {
        var request = new HttpRequestMessage(httpMethod, uri);
        var response = await httpClient.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();
        
        return content;
    }
}