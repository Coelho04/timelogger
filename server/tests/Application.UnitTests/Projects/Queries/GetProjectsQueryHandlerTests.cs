using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Projects.Models;
using Microsoft.Extensions.DependencyInjection.Projects.Queries.GetProjects;
using MockQueryable.Moq;
using Moq;
using Timelogger.Application.Common.Interfaces;
using Timelogger.Application.Common.Models;
using Timelogger.Domain.Entities;
using Xunit;

namespace Timelogger.Application.UnitTests.TimeRegistrations.Queries;

public class GetProjectsQueryHandlerTests
{
    private readonly Mock<DbSet<Project>> _projects = new();
    private readonly Mock<IApplicationDbContext> _databaseContext = new();
    private readonly GetProjectsQueryHandler _handler;

    public GetProjectsQueryHandlerTests()
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile<ProjectDto.Mapping>();
        });

        IMapper mapper = mappingConfig.CreateMapper();
        _handler = new GetProjectsQueryHandler(_databaseContext.Object, mapper);
    }

    [Fact]
    public async Task Handle_ShouldReturnCorrectPaginatedList_WhenValidQuery()
    {
        // Arrange
        var data = new Project[] { new() { Id = 1, Name = "Project_1", Deadline = new DateOnly() }, new() { Id = 2, Name = "Project_2", Deadline = new DateOnly() } };

        this.MockProjectDbSet(data);

        var query = new GetProjectsQuery();

        // Act
        var result = await _handler.Handle(query, It.IsAny<CancellationToken>());

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PaginatedList<ProjectDto>>(result);
        Assert.Equal(2, result.TotalCount);
        Assert.Equal(1, result.PageNumber);
        Assert.Equal(1, result.TotalPages);
        Assert.False(result.HasNextPage);
        Assert.False(result.HasPreviousPage);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnCorrectPaginatedList_WhenValidQueryWithNameParameter()
    {
        // Arrange
        var data = new Project[] { new() { Id = 1, Name = "Project_1", Deadline = new DateOnly() }, new() { Id = 2, Name = "Project_2", Deadline = new DateOnly() } };

        this.MockProjectDbSet(data);

        var query = new GetProjectsQuery("1");

        // Act
        var result = await _handler.Handle(query, It.IsAny<CancellationToken>());

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PaginatedList<ProjectDto>>(result);
        Assert.Equal(1, result.TotalCount);
        Assert.Equal(1, result.PageNumber);
        Assert.Equal(1, result.TotalPages);
        Assert.False(result.HasNextPage);
        Assert.False(result.HasPreviousPage);
    }

    private void MockProjectDbSet(Project[]? projects = null)
    {
        projects ??= [];

        var projectsMock = projects.AsQueryable().BuildMock();

        this._projects.As<IQueryable<Project>>().Setup(m => m.Provider)
            .Returns(projectsMock.Provider);
        this._projects.As<IQueryable<Project>>().Setup(m => m.Expression)
            .Returns(projectsMock.Expression);
        this._projects.As<IQueryable<Project>>().Setup(m => m.ElementType)
            .Returns(projectsMock.ElementType);
        this._projects.As<IQueryable<Project>>().Setup(m => m.GetEnumerator())
            .Returns(() => projectsMock.GetEnumerator());

        this._databaseContext.Setup(x => x.Projects).Returns(this._projects.Object);
    }
}
