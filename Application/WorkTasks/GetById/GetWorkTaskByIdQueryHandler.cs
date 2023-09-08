using Application.WorkTasks.Responses;
using Domain.WorkTasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.WorkTasks.GetById;

internal sealed class GetWorkTaskByIdQueryHandler : IRequestHandler<GetWorkTaskByIdQuery, WorkTaskResponse>
{
    private readonly ApplicationDbContext _context;

    public GetWorkTaskByIdQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<WorkTaskResponse> Handle(GetWorkTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var workTask = await _context
            .WorkTasks
            .Where(wt => wt.Id == request.WorkTaskId)
            .Select(wt => new WorkTaskResponse(
                wt.Id.Value,
                wt.Description,
                wt.CategoryL1Id))
            .FirstOrDefaultAsync(cancellationToken);

        if (workTask == null)
        {
            throw new WorkTaskNotFoundException(request.WorkTaskId);
        }

        return workTask;


    }
}