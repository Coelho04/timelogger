namespace Microsoft.Extensions.DependencyInjection.TimeRegistrations.Commands.Delete;

public record DeleteTimeRegistrationCommand(int Id) : IRequest;
