using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Projects.Models;
using Microsoft.Extensions.DependencyInjection.TimeRegistrations.Queries.GetTimeRegistrations;
using Moq;
using Moq.EntityFrameworkCore;
using Timelogger.Application.Common.Interfaces;
using Timelogger.Application.Common.Models;
using Timelogger.Domain.Entities;
using Xunit;

namespace Timelogger.Application.UnitTests.TimeRegistrations.Queries;

public class GetTimeRegistrationsQueryHandlerTests
{
    private Mock<IApplicationDbContext> _mockContext;
    private GetTimeRegistrationsQueryHandler _handler;
    
    public GetTimeRegistrationsQueryHandlerTests()
    {
        _mockContext = new Mock<IApplicationDbContext>();
        _mockContext.Setup<DbSet<TimeRegistration>>(s => s.TimeRegistrations).ReturnsDbSet(new List<TimeRegistration>
        {
            new TimeRegistration { Id = 1, ProjectId = 1 },
            new TimeRegistration { Id = 2, ProjectId = 1 }
        });
        
        
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile<TimeRegistrationDto.Mapping>();
        });
        
        IMapper mapper = mappingConfig.CreateMapper();
        _handler = new GetTimeRegistrationsQueryHandler(_mockContext.Object, mapper);
    }
    
    [Fact(Skip = "Need to be fixed")]
    public async Task Handle_ShouldReturnCorrectPaginatedList_WhenValidQuery()
    {
        // Arrange
        var query = new GetTimeRegistrationsQuery(1);
        
        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PaginatedList<TimeRegistrationDto>>(result);
        Assert.Equal(2, result.Items.Count());
    }
}
