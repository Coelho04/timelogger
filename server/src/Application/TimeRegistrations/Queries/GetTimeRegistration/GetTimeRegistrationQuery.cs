using Microsoft.Extensions.DependencyInjection.Projects.Models;

namespace Microsoft.Extensions.DependencyInjection.TimeRegistrations.Queries.GetTimeRegistration;

public record GetTimeRegistrationQuery(int Id) : IRequest<TimeRegistrationDto>;
