namespace Domain.WorkTasks;

public sealed class WorkTaskNotFoundException : Exception
{
    public WorkTaskNotFoundException(WorkTaskId id)
        :base($"The task with the ID = {id.Value} was not found.")
    {
    }
}
