using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Projects.Commands.Create;
using Microsoft.Extensions.DependencyInjection.Projects.Commands.Delete;
using Microsoft.Extensions.DependencyInjection.Projects.Commands.Update;
using Microsoft.Extensions.DependencyInjection.Projects.Queries.GetProjects;

namespace Microsoft.Extensions.DependencyInjection.Endpoints;

public static class Projects
{
    public static IEndpointRouteBuilder MapProjects(this IEndpointRouteBuilder builder)
    {
        var group = builder
            .MapGroup("/projects")
            .WithTags("Projects");
        
        group.MapGet("/", async ([AsParameters] GetProjectsQuery query, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var result = await mediator.Send(query, cancellationToken);

                return TypedResults.Ok(result);
            })
            .WithOpenApi(operation =>
            {
                operation.Description = "Get Projects";
                operation.Summary = "Get Projects";
                
                return operation;
            })
            .WithName("Get Projects");
        
        group.MapPost("/", async ([FromBody] CreateProjectCommand command, IMediator mediator, CancellationToken cancellationToken) =>
            {
                await mediator.Send(command, cancellationToken);

                return TypedResults.NoContent();
            })
            .WithOpenApi(operation =>
            {
                operation.Description = "Create Project";
                operation.Summary = "Create Project";
                
                return operation;
            })
            .WithName("Create Project");

        group.MapPut("/", async ([FromBody] UpdateProjectCommand command, IMediator mediator, CancellationToken cancellationToken) =>
            {
                await mediator.Send(command, cancellationToken);

                return TypedResults.NoContent();
            })
            .WithOpenApi(operation =>
            {
                operation.Description = "Update Project";
                operation.Summary = "Update Project";
                
                return operation;
            })
            .WithName("Update Project");
        
        group.MapDelete("/{id:int}", async (int id, IMediator mediator, CancellationToken cancellationToken) =>
            {
                await mediator.Send(new DeleteProjectCommand(id), cancellationToken);

                return TypedResults.NoContent();
            })
            .WithOpenApi(operation =>
            {
                operation.Description = "Delete Project";
                operation.Summary = "Delete Project";
                
                return operation;
            })
            .WithName("Delete Project");
        
        return group;
    }
}
