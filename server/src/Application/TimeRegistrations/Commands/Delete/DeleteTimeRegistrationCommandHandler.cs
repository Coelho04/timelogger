using Microsoft.Extensions.DependencyInjection.Projects.Commands.Delete;
using Timelogger.Application.Common.Interfaces;

namespace Microsoft.Extensions.DependencyInjection.TimeRegistrations.Commands.Delete;


public class DeleteTimeRegistrationCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteTimeRegistrationCommand>
{
    public async Task Handle(DeleteTimeRegistrationCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.TimeRegistrations
            .FindAsync([request.Id], cancellationToken);
        
        Guard.Against.NotFound(request.Id, entity);

        context.TimeRegistrations.Remove(entity);

        await context.SaveChangesAsync(cancellationToken);
    }
}
