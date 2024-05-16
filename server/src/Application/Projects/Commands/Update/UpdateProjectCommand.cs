namespace Microsoft.Extensions.DependencyInjection.Projects.Commands.Update;

public record UpdateProjectCommand(int Id, string Name, DateOnly Deadline, bool IsCompleted) : IRequest;
