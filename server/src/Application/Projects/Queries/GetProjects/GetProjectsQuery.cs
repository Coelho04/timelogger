using Microsoft.Extensions.DependencyInjection.Projects.Models;
using Timelogger.Application.Common.Models;

namespace Microsoft.Extensions.DependencyInjection.Projects.Queries.GetProjects;

public record GetProjectsQuery(string? Name = null, int PageNumber = 1, int PageSize = 10, string Sort = "id") : IRequest<PaginatedList<ProjectDto>>;

