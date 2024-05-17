using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Projects.Models;
using Microsoft.Extensions.DependencyInjection.TimeRegistrations.Queries.GetTimeRegistrations;
using MockQueryable.Moq;
using Moq;
using Timelogger.Application.Common.Interfaces;
using Timelogger.Application.Common.Models;
using Timelogger.Domain.Entities;
using Xunit;

namespace Timelogger.Application.UnitTests.TimeRegistrations.Queries;

public class GetTimeRegistrationsQueryHandlerTests
{
    private readonly Mock<DbSet<TimeRegistration>> _timeRegistrations = new();
    private readonly Mock<IApplicationDbContext> _databaseContext = new();
    private readonly GetTimeRegistrationsQueryHandler _handler;

    public GetTimeRegistrationsQueryHandlerTests()
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile<TimeRegistrationDto.Mapping>();
        });

        IMapper mapper = mappingConfig.CreateMapper();
        _handler = new GetTimeRegistrationsQueryHandler(_databaseContext.Object, mapper);
    }

    [Fact]
    public async Task Handle_ShouldReturnCorrectPaginatedList_WhenValidQuery()
    {
        // Arrange
        var data = new TimeRegistration[] { new() { Id = 1, ProjectId = 1 }, new() { Id = 2, ProjectId = 1 } };

        this.MockTimeRegistrationDbSet(data);

        var query = new GetTimeRegistrationsQuery(1);

        // Act
        var result = await _handler.Handle(query, It.IsAny<CancellationToken>());

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PaginatedList<TimeRegistrationDto>>(result);
        Assert.Equal(2, result.TotalCount);
        Assert.Equal(1, result.PageNumber);
        Assert.Equal(1, result.TotalPages);
        Assert.False(result.HasNextPage);
        Assert.False(result.HasPreviousPage);
    }

    private void MockTimeRegistrationDbSet(TimeRegistration[]? timeRegistrations = null)
    {
        timeRegistrations ??= [];

        var timeRegistrationsMock = timeRegistrations.AsQueryable().BuildMock();

        this._timeRegistrations.As<IQueryable<TimeRegistration>>().Setup(m => m.Provider)
            .Returns(timeRegistrationsMock.Provider);
        this._timeRegistrations.As<IQueryable<TimeRegistration>>().Setup(m => m.Expression)
            .Returns(timeRegistrationsMock.Expression);
        this._timeRegistrations.As<IQueryable<TimeRegistration>>().Setup(m => m.ElementType)
            .Returns(timeRegistrationsMock.ElementType);
        this._timeRegistrations.As<IQueryable<TimeRegistration>>().Setup(m => m.GetEnumerator())
            .Returns(() => timeRegistrationsMock.GetEnumerator());

        this._databaseContext.Setup(x => x.TimeRegistrations).Returns(this._timeRegistrations.Object);
    }
}
