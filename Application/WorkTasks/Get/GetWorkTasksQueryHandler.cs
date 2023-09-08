using Application.Shared;
using Application.WorkTasks.Responses;
using Domain.WorkTasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Linq.Expressions;

namespace Application.WorkTasks.Get;

internal sealed class GetWorkTasksQueryHandler : IRequestHandler<GetWorkTasksQuery, PagedList<WorkTaskResponse>>
{
    private readonly ApplicationDbContext _context;

    public GetWorkTasksQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedList<WorkTaskResponse>> Handle(GetWorkTasksQuery request, CancellationToken cancellationToken)
    {
        IQueryable<WorkTask> workTasksQuery = _context.WorkTasks;

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            workTasksQuery = workTasksQuery.Where(wt =>
                wt.Description != null && wt.Description.Contains(request.SearchTerm));
        }

        var keySelector = GetSortProperty(request);

        if (request.SortOrder?.ToLower() == "desc")
        {
            workTasksQuery = workTasksQuery.OrderByDescending(keySelector);
        }
        else
        {
            workTasksQuery = workTasksQuery.OrderBy(keySelector);
        }

        var workTaskResponsesQuery = workTasksQuery
            .Select(wt => new WorkTaskResponse(
                wt.Id.Value,
                wt.Description,
                wt.CategoryL1Id));

        var workTasks = await PagedList<WorkTaskResponse>.CreateAsync(workTaskResponsesQuery, request.Page, request.PageSize);

        return workTasks;
    }

    private static Expression<Func<WorkTask, object>> GetSortProperty(GetWorkTasksQuery request)
    {
        return request.SortColumn?.ToLower() switch
        {
            "description" => wt => wt.Description,
            "categoryl1" => wt => wt.CategoryL1Id,
            _ => wt => wt.Id
        };
    }
}