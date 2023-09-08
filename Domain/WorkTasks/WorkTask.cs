namespace Domain.WorkTasks;

public class WorkTask
{
    public WorkTaskId Id { get; private set; }
    public string? Description { get; private set; }
    public int CategoryL1Id { get; private set; }
    public double RunTime { get; private set; }
    public DateTimeOffset CreatedOn { get; private set; }
    public DateTimeOffset? UpdatedOn { get; private set; }

    public void Update(string? description, int categoryL1Id, double runTime)
    {
        Description = description;
        CategoryL1Id = categoryL1Id;
        RunTime = runTime;
    }

    public static WorkTask Create(string? description, int categoryL1Id)
    {
        var workTask = new WorkTask
        {
            Id = new WorkTaskId(Guid.NewGuid()),
            Description = description,
            CategoryL1Id = categoryL1Id,
            CreatedOn = DateTime.UtcNow
        };

        return workTask;
    }
}
