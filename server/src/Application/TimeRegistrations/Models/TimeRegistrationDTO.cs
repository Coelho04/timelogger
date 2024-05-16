using Timelogger.Domain.Entities;

namespace Microsoft.Extensions.DependencyInjection.Projects.Models;

public record TimeRegistrationDto(int Id, double Duration, string Description)
{
    private class Mapping : Profile
    {
        public Mapping() => this.CreateMap<TimeRegistration, TimeRegistrationDto>();
    }
}
