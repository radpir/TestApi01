using Application.WorkTasks.Create;
using Application.WorkTasks.Delete;
using Application.WorkTasks.Get;
using Application.WorkTasks.GetById;
using Application.WorkTasks.Update;
using Domain.WorkTasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.API.Abstractions;

namespace Web.API.Endpoints;

public class WorkTaskEndpointDefinition : IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        app.MapGet("/worktasks/{id:guid}", async (Guid id, ISender sender) =>
        {
            try
            {
                var workTask = await sender.Send(new GetWorkTaskByIdQuery(new WorkTaskId(id)));

                return Results.Ok(workTask);
            }
            catch (WorkTaskNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        })
        .WithName("GetTask")
        .WithOpenApi();

        app.MapGet("/worktasks", async (string? searchTerm, string? sortColumn, string? sortOrder, int page, int pageSize, ISender sender) =>
        {
            var query = new GetWorkTasksQuery(searchTerm, sortColumn, sortOrder, page, pageSize);

            var workTasks = await sender.Send(query);

            return Results.Ok(workTasks);
        })
        .WithName("GetTasks")
        .WithOpenApi();

        app.MapPost("/worktasks", async (CreateWorkTaskCommand command, ISender sender) =>
        {
            await sender.Send(command);

            return Results.Ok();
        });

        app.MapPut("/worktasks/{id:guid}", async (Guid id, [FromBody] UpdateWorkTaskRequest request, ISender sender) =>
        {
            var command = new UpdateWorkTaskCommand(
                new WorkTaskId(id),
                request.Description,
                request.CategoryL1Id,
                request.RunTime);

            await sender.Send(command);

            return Results.NoContent();
        });

        app.MapDelete("/worktasks/{id:guid}", async (Guid id, ISender sender) =>
        {
            try
            {
                await sender.Send(new DeleteWorkTasksCommand(new WorkTaskId(id)));

                return Results.NoContent();
            }
            catch (WorkTaskNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });
    }
}
