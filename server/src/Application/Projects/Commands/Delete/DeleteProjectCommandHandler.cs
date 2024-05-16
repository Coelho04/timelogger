using Timelogger.Application.Common.Interfaces;

namespace Microsoft.Extensions.DependencyInjection.Projects.Commands.Delete;

public class DeleteProjectCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteProjectCommand>
{
    public async Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Projects
            .FindAsync([request.Id], cancellationToken);
        
        Guard.Against.NotFound(request.Id, entity);

        context.Projects.Remove(entity);

        await context.SaveChangesAsync(cancellationToken);
    }
}
