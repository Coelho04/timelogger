using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Timelogger.Infrastructure.IntegrationTests.Helpers;
using Xunit;

namespace Timelogger.Infrastructure.IntegrationTests.Endpoints;

public class ProjectsEndpointsTests(TimeloggerApiWebApplicationFactory<Program> factory)
    : IClassFixture<TimeloggerApiWebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient = factory.CreateClient();

    private static readonly JsonSerializerOptions DefaultWebJsonOptions = new(JsonSerializerDefaults.Web);
    
    [Fact]
    public async Task AddProject_ReturnsOk()
    {
        // Arrange
        var project = new
        {
            name = "Project 1",
            deadline = DateOnly.FromDateTime(DateTime.Now.AddDays(2)),
        };

        // Act
        var response = await this._httpClient.PostAsJsonAsync("/projects/create", project);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task GetProjects_ReturnsProjects()
    {
        // Arrange
        // Act
        var response = await this._httpClient.GetAsync("/projects");
        var list = JsonSerializer.Deserialize<PaginatedList<Project>>(await response.Content.ReadAsStringAsync(), DefaultWebJsonOptions);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(list);
        Assert.NotEmpty(list.Items!);
    }
    
    
    public class PaginatedList<T>
    {
        public IReadOnlyCollection<T> Items { get; set; } = [];
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }

    public record Project(int Id, string Name, DateOnly Deadline, bool IsCompleted);
}
