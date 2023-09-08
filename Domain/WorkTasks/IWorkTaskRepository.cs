namespace Domain.WorkTasks;

public interface IWorkTaskRepository
{
    Task<WorkTask?> GetByIdAsync(WorkTaskId id);
    void Add(WorkTask workTask);
    void Update(WorkTask workTask);
    void Remove(WorkTask workTask);
}
