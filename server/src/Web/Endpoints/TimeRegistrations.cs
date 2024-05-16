using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Projects.Commands.Create;
using Microsoft.Extensions.DependencyInjection.Projects.Commands.Delete;
using Microsoft.Extensions.DependencyInjection.Projects.Commands.Update;
using Microsoft.Extensions.DependencyInjection.TimeRegistrations.Commands.Create;
using Microsoft.Extensions.DependencyInjection.TimeRegistrations.Commands.Delete;
using Microsoft.Extensions.DependencyInjection.TimeRegistrations.Commands.Update;
using Microsoft.Extensions.DependencyInjection.TimeRegistrations.Queries.GetTimeRegistrations;

namespace Microsoft.Extensions.DependencyInjection.Endpoints;

public static class TimeRegistrations
{
    public static IEndpointRouteBuilder MapTimeRegistrations(this IEndpointRouteBuilder builder)
    {
        var group = builder
            .MapGroup("/time-registrations")
            .WithTags("Time Registrations");
        
        group.MapGet("/", async ([AsParameters] GetTimeRegistrationsQuery query, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(query, cancellationToken);

                return TypedResults.Ok(result);
            })
            .WithOpenApi(operation =>
            {
                operation.Description = "Get Time Registrations";
                operation.Summary = "Get Time Registrations";
                
                return operation;
            })
            .WithName("Get Time Registrations");
        
        group.MapPost("/", async ([FromBody] CreateTimeRegistrationCommand command, IMediator mediator, CancellationToken cancellationToken) =>
            {
                await mediator.Send(command, cancellationToken);

                return TypedResults.Created();
            })
            .WithOpenApi(operation =>
            {
                operation.Description = "Create Time Registration";
                operation.Summary = "Create Time Registration";
                
                return operation;
            })
            .WithName("Create Time Registration");

        group.MapPut("/", async ([FromBody] UpdateTimeRegistrationCommand command, IMediator mediator, CancellationToken cancellationToken) =>
            {
                await mediator.Send(command, cancellationToken);

                return TypedResults.NoContent();
            })
            .WithOpenApi(operation =>
            {
                operation.Description = "Update Time Registration";
                operation.Summary = "Update Time Registration";
                
                return operation;
            })
            .WithName("Update Time Registration");
        
        group.MapDelete("/{id:int}", async (int id, IMediator mediator, CancellationToken cancellationToken) =>
            {
                await mediator.Send(new DeleteTimeRegistrationCommand(id), cancellationToken);

                return TypedResults.NoContent();
            })
            .WithOpenApi(operation =>
            {
                operation.Description = "Delete Time Registration";
                operation.Summary = "Delete Time Registration";
                
                return operation;
            })
            .WithName("Delete Time Registration");
        
        return group;
    }
}
