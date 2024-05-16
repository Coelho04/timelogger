namespace Microsoft.Extensions.DependencyInjection.TimeRegistrations.Commands.Create;

public record CreateTimeRegistrationCommand(int ProjectId, double Duration, string Description)
    : IRequest<int>;
