using Domain.WorkTasks;
using MediatR;

namespace Application.WorkTasks.Delete;

public record DeleteWorkTasksCommand(WorkTaskId WorkTaskId) : IRequest;
