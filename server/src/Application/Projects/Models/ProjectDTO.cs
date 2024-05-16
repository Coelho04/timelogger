using Timelogger.Domain.Entities;

namespace Microsoft.Extensions.DependencyInjection.Projects.Models;

public record ProjectDto(int Id, string Name, DateOnly Deadline, bool IsCompleted)
{
    private class Mapping : Profile
    {
        public Mapping() => this.CreateMap<Project, ProjectDto>();
    }
}
