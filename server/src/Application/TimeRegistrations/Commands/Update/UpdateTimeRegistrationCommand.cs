namespace Microsoft.Extensions.DependencyInjection.TimeRegistrations.Commands.Update;

public record UpdateTimeRegistrationCommand(int Id, int ProjectId, double Duration, string Description) : IRequest;
