using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timelogger.Domain.Entities;

namespace Timelogger.Infrastructure.Data.Configurations;

public class TimeRegistrationConfiguration: IEntityTypeConfiguration<TimeRegistration>
{
    public void Configure(EntityTypeBuilder<TimeRegistration> builder)
    {
        builder.HasOne<Project>(b => b.Project)
            .WithMany(b => b.TimeRegistrations)
            .HasForeignKey(p => p.ProjectId);
        
        builder.Property(t => t.Duration)
            .IsRequired();
        
        builder.Property(t => t.Description)
            .HasMaxLength(200)
            .IsRequired();
    }
}
