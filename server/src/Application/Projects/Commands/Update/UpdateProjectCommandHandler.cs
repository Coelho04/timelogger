using Timelogger.Application.Common.Interfaces;

namespace Microsoft.Extensions.DependencyInjection.Projects.Commands.Update;

public class UpdateProjectCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateProjectCommand>
{
    public async Task Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Projects
            .FindAsync([request.Id], cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        if(entity.Name != request.Name)
        {
            var isNameTaken = await context.Projects
                .AnyAsync(x => x.Name == request.Name, cancellationToken);

            if(isNameTaken)
            {
                throw new Exception("Project name is already taken.");
            }
        }

        if (entity.Deadline != request.Deadline & request.Deadline < DateOnly.FromDateTime(DateTime.Now))
        {
            throw new Exception("Deadline must be in the future.");
        }
        
        entity.Name = request.Name;
        entity.Deadline = request.Deadline;
        entity.IsCompleted = request.IsCompleted;

        await context.SaveChangesAsync(cancellationToken);
    }
}
