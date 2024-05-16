using Microsoft.Extensions.DependencyInjection.Projects.Models;
using Timelogger.Application.Common.Models;

namespace Microsoft.Extensions.DependencyInjection.TimeRegistrations.Queries.GetTimeRegistrations;

public record GetTimeRegistrationsQuery(int ProjectId, int PageNumber = 1, int PageSize = 10)
    : IRequest<PaginatedList<TimeRegistrationDto>>;


