using Timelogger.Application.Common.Interfaces;
using Timelogger.Domain.Entities;

namespace Microsoft.Extensions.DependencyInjection.TimeRegistrations.Commands.Create;

public class CreateTimeRegistrationCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateTimeRegistrationCommand, int>
{
    public async Task<int> Handle(CreateTimeRegistrationCommand request, CancellationToken cancellationToken)
    {
        var entity = new TimeRegistration
        {
            ProjectId = request.ProjectId,
            Description = request.Description,
            Duration = request.Duration,
        };

        context.TimeRegistrations.Add(entity);

        await context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
