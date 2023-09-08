namespace Application.WorkTasks.Responses;

public record WorkTaskResponse(
    Guid id,
    string? Description,
    int CategoryL1Id);

