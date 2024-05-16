using Microsoft.Extensions.DependencyInjection.Projects.Models;
using Microsoft.Extensions.DependencyInjection.Projects.Queries.GetProjects;
using Timelogger.Application.Common.Interfaces;
using Timelogger.Application.Common.Mappings;
using Timelogger.Application.Common.Models;

namespace Microsoft.Extensions.DependencyInjection.TimeRegistrations.Queries.GetTimeRegistrations;

public class GetTimeRegistrationsQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetTimeRegistrationsQuery, PaginatedList<TimeRegistrationDto>>
{
    public async Task<PaginatedList<TimeRegistrationDto>> Handle(GetTimeRegistrationsQuery request,
        CancellationToken cancellationToken)
    {
        (int projectId, int pageNumber, int pageSize) = request;
        
        return await context.TimeRegistrations
            .Where(w => w.ProjectId == projectId)
            .OrderBy(o => o.Id)
            .ProjectTo<TimeRegistrationDto>(mapper.ConfigurationProvider)
            .PaginatedListAsync(pageNumber, pageSize);
    }
    
}
