using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Timelogger.Infrastructure.IntegrationTests.Helpers;

public class TimeloggerApiWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        // builder.ConfigureServices(services =>
        // {
        //     var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DatabaseContext>));
        //     
        //     if (descriptor != null)
        //     {
        //         services.Remove(descriptor);
        //     }
        //     
        //     services.AddDbContext<DatabaseContext>((sp, options) =>
        //     {
        //         options.UseSqlite("Data Source=auctions_test.db");
        //     });
        // });

        var host = base.CreateHost(builder);
        
        return host;
    }
}
