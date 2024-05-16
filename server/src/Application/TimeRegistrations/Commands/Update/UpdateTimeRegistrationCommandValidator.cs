using Microsoft.Extensions.DependencyInjection.TimeRegistrations.Commands.Create;
using Timelogger.Application.Common.Interfaces;

namespace Microsoft.Extensions.DependencyInjection.TimeRegistrations.Commands.Update;

public class UpdateTimeRegistrationCommandValidator: AbstractValidator<UpdateTimeRegistrationCommand>
{
    private readonly IApplicationDbContext _context;
    
    public UpdateTimeRegistrationCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        
        RuleFor(v => v.Duration)
            .GreaterThanOrEqualTo(0.5);
        
        RuleFor(v => v.Description)
            .MaximumLength(200)
            .NotEmpty();
        
        RuleFor(v => v.ProjectId)
            .MustAsync(ProjectExists)
            .WithMessage("Project does not exists.")
            .WithErrorCode("ProjectNotFound")
            .MustAsync(ProjectIsNotCompleted)
            .WithMessage("Project is already completed.")
            .WithErrorCode("ProjectIsCompleted");
    }
    
    private async Task<bool> ProjectExists(int id, CancellationToken cancellationToken)
    {
        return await _context.Projects
            .FindAsync([id], cancellationToken) is not null;
    }
    
    private async Task<bool> ProjectIsNotCompleted(int id, CancellationToken cancellationToken)
    {
        var entity = await _context.Projects
            .FindAsync([id], cancellationToken);

        return !entity!.IsCompleted;
    }
}
