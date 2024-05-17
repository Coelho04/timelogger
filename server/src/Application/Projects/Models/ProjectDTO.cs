using Timelogger.Domain.Entities;

namespace Microsoft.Extensions.DependencyInjection.Projects.Models;

public record ProjectDto(int Id, string Name, DateOnly Deadline, bool IsCompleted)
{
    // TODO (personal opinion)
    // Remove Automapper and use a static method to map from Project to ProjectDto
    public class Mapping : Profile
    {
        public Mapping() => this.CreateMap<Project, ProjectDto>();
    }
}
