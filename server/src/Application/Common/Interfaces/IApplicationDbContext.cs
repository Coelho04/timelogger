using Timelogger.Domain.Entities;

namespace Timelogger.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Project> Projects { get; }

    DbSet<TimeRegistration> TimeRegistrations { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
