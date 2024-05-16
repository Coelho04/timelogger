using Timelogger.Application.Common.Interfaces;
using Timelogger.Domain.Entities;

namespace Microsoft.Extensions.DependencyInjection.Projects.Commands.Create;

public class CreateProjectCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateProjectCommand, int>
{
         public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
     {
         var entity = new Project
         {
             Name = request.Name,
             Deadline = request.Deadline
         };

         context.Projects.Add(entity);

         await context.SaveChangesAsync(cancellationToken);

         return entity.Id;
     }
}
