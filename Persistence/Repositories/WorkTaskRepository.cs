using Domain.WorkTasks;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class WorkTaskRepository : IWorkTaskRepository
{
    private readonly ApplicationDbContext _context;

    public WorkTaskRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(WorkTask workTask)
    {
        _context.WorkTasks.Add(workTask);
    }

    public async Task<WorkTask?> GetByIdAsync(WorkTaskId id)
    {
        var workTask = await _context.WorkTasks.FirstOrDefaultAsync(x => x.Id == id);

        return workTask;
    }

    public void Remove(WorkTask workTask)
    {
        _context.WorkTasks.Remove(workTask);
    }

    public void Update(WorkTask workTask)
    {
        _context.WorkTasks.Update(workTask);
    }
}
