using Microsoft.Extensions.DependencyInjection.Projects.Models;
using Timelogger.Domain.Entities;

namespace Microsoft.Extensions.DependencyInjection.Projects.Queries.GetProject;

public record GetProjectQuery(int Id) : IRequest<ProjectDto>
{
    
}
