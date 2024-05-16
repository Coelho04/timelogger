namespace Microsoft.Extensions.DependencyInjection.Projects.Commands.Create;

public record CreateProjectCommand(string Name, DateOnly Deadline) : IRequest<int>;
