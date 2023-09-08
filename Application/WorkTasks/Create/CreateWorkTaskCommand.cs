using MediatR;

namespace Application.WorkTasks.Create;

public record CreateWorkTaskCommand(string Description, int CategoryL1Id) : IRequest;
