using Microsoft.Extensions.DependencyInjection.Projects.Models;
using Timelogger.Application.Common.Interfaces;

namespace Microsoft.Extensions.DependencyInjection.TimeRegistrations.Queries.GetTimeRegistration;

public class GetTimeRegistrationQueryHandler(IApplicationDbContext context) : IRequestHandler<GetTimeRegistrationQuery, TimeRegistrationDto>
{
    public async Task<TimeRegistrationDto> Handle(GetTimeRegistrationQuery request, CancellationToken cancellationToken)
    {
        var entity = await context.TimeRegistrations
            .FindAsync([request.Id], cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        return new TimeRegistrationDto(entity.Id, entity.Duration, entity.Description!);
    }
}
