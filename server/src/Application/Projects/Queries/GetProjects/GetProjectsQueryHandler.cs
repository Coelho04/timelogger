using Microsoft.Extensions.DependencyInjection.Projects.Models;
using Timelogger.Application.Common.Interfaces;
using Timelogger.Application.Common.Mappings;
using Timelogger.Application.Common.Models;
using Timelogger.Domain.Entities;

namespace Microsoft.Extensions.DependencyInjection.Projects.Queries.GetProjects;

public class GetProjectsQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetProjectsQuery, PaginatedList<ProjectDto>>
{
    public async Task<PaginatedList<ProjectDto>> Handle(GetProjectsQuery request,
        CancellationToken cancellationToken)
    {
        (string? name, int pageNumber, int pageSize, string sort) = request;
        
        return await context.Projects.Where(c =>
                // TODO - Temporary fix for EF.Functions.Like not working on unit tests
                //name == null || EF.Functions.Like(c.Name, $"%{name}%"))
                (name == null || (!string.IsNullOrWhiteSpace(c.Name) && c.Name.Contains(name))))
            .OrderProjects(sort)
            .ProjectTo<ProjectDto>(mapper.ConfigurationProvider)
            .PaginatedListAsync(pageNumber, pageSize);
    }
    
}

public static class QueryableExtensions
{
    public static IOrderedQueryable<Project> OrderProjects(this IQueryable<Project> projects, string sort)
    {
        return sort switch
        {
            "id_desc" => projects.OrderByDescending(p => p.Id),
            "deadline" => projects.OrderBy(p => p.Deadline),
            "deadline_desc" => projects.OrderByDescending(p => p.Deadline),
            _ => projects.OrderBy(p => p.Id)
        };
    }
}
