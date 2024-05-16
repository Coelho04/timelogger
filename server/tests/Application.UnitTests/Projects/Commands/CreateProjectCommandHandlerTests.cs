using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Projects.Commands.Create;
using Moq;
using Timelogger.Application.Common.Interfaces;
using Xunit;
using Moq.EntityFrameworkCore;
using Timelogger.Domain.Entities;

namespace Timelogger.Application.UnitTests.Projects.Commands;

public class CreateProjectCommandHandlerTests
{
    private Mock<IApplicationDbContext> _mockContext;
    private CreateProjectCommandHandler _handler;

    public CreateProjectCommandHandlerTests()
    {
        _mockContext = new Mock<IApplicationDbContext>();
        _mockContext.Setup<DbSet<Project>>(s => s.Projects).ReturnsDbSet(new List<Project>());
        
        _handler = new CreateProjectCommandHandler(_mockContext.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ShouldCreateProject()
    {
        // Arrange
        var command = new CreateProjectCommand("Test Project", DateOnly.FromDateTime(DateTime.Now.AddDays(7)));

        // Act
        var projectId = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}
