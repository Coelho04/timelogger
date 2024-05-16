using System.Reflection;
using Timelogger.Application.Common.Interfaces;
using Timelogger.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Timelogger.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<Project> Projects => Set<Project>();

    public DbSet<TimeRegistration> TimeRegistrations => Set<TimeRegistration>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
