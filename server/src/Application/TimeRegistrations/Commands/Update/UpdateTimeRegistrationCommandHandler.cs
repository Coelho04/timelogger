using Timelogger.Application.Common.Interfaces;

namespace Microsoft.Extensions.DependencyInjection.TimeRegistrations.Commands.Update;

public class UpdateTimeRegistrationCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateTimeRegistrationCommand>
{
    public async Task Handle(UpdateTimeRegistrationCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.TimeRegistrations
            .FindAsync([request.Id], cancellationToken);
        
        Guard.Against.NotFound(request.Id, entity);
        
        entity.ProjectId = request.ProjectId;
        entity.Description = request.Description;
        entity.Duration = request.Duration;
        entity.Description = request.Description;
        
        await context.SaveChangesAsync(cancellationToken);
    }
}
