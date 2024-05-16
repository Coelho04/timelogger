using Microsoft.Extensions.DependencyInjection.Endpoints;

namespace Timelogger.Web.Infrastructure;

public static class WebApplicationExtensions
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapProjects();
        app.MapTimeRegistrations();

        return app;
    }
}
