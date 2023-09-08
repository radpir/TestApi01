using Domain.WorkTasks;
using MediatR;

namespace Application.WorkTasks.Update;

public record UpdateWorkTaskCommand(
    WorkTaskId WorkTaskId,
    string? Description,
    int CategoryL1Id,
    double RunTime) : IRequest;

public record UpdateWorkTaskRequest(
    string? Description,
    int CategoryL1Id,
    double RunTime);
