using Timelogger.Application.Common.Interfaces;

namespace Microsoft.Extensions.DependencyInjection.Projects.Commands.Create;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    private readonly IApplicationDbContext _context;

     public CreateProjectCommandValidator(IApplicationDbContext context)
     {
         _context = context;

         RuleFor(v => v.Deadline).Must(BiggerThanCurrentDate)
             .WithMessage("'{PropertyName}' must be bigger than current date.")
             .WithErrorCode("LowerThanCurrentDate");
         
         RuleFor(v => v.Name)
             .NotEmpty()
             .MaximumLength(200)
             .MustAsync(UniqueProject)
             .WithMessage("'{PropertyName}' must be unique.")
             .WithErrorCode("Unique");
     }

     private bool BiggerThanCurrentDate(DateOnly date) 
     {
         return date >= DateOnly.FromDateTime(DateTime.UtcNow);
     }

     private async Task<bool> UniqueProject(string name, CancellationToken cancellationToken)
     {
         return await _context.Projects
             .AllAsync(l => l.Name != name, cancellationToken);
     }
}
