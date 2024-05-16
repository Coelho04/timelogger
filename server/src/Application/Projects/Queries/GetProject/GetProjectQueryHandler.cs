using Microsoft.Extensions.DependencyInjection.Projects.Models;
using Timelogger.Application.Common.Interfaces;

namespace Microsoft.Extensions.DependencyInjection.Projects.Queries.GetProject;

public class GetProjectQueryHandler(IApplicationDbContext context) : IRequestHandler<GetProjectQuery, ProjectDto>
{
    public async Task<ProjectDto> Handle(GetProjectQuery request, CancellationToken cancellationToken)
    {
        var entity = await context.Projects
            .FindAsync([request.Id], cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        return new ProjectDto(entity.Id, entity.Name!, entity.Deadline, entity.IsCompleted);
    }
}
