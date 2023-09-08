using Application.WorkTasks.Responses;
using Domain.WorkTasks;
using MediatR;

namespace Application.WorkTasks.GetById;

public record GetWorkTaskByIdQuery(WorkTaskId WorkTaskId) : IRequest<WorkTaskResponse>;