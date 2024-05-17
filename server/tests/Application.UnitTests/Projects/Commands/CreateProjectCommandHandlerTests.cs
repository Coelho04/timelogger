using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection.Projects.Commands.Create;
using Moq;
using Timelogger.Application.Common.Interfaces;
using Xunit;
using Timelogger.Domain.Entities;

namespace Timelogger.Application.UnitTests.Projects.Commands;

public class CreateProjectCommandHandlerTests
{
    private readonly Mock<DbSet<Project>> _projects = new();
    private readonly Mock<IApplicationDbContext> _databaseContext = new();
    private readonly CreateProjectCommandHandler _handler;

    public CreateProjectCommandHandlerTests()
    {
        _handler = new CreateProjectCommandHandler(_databaseContext.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ShouldCreateProject()
    {
        // Arrange
        _projects.Setup(s => s.AddAsync(It.IsAny<Project>(), It.IsAny<CancellationToken>()))
            .Returns(new ValueTask<EntityEntry<Project>>());
        
        this._databaseContext.Setup(s => s.Projects).Returns(_projects.Object);
        
        var command = new CreateProjectCommand("Test Project", DateOnly.FromDateTime(DateTime.Now.AddDays(7)));

        // Act
        _ = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _databaseContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
    
}
