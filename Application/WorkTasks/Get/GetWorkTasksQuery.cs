using Application.Shared;
using Application.WorkTasks.Responses;
using MediatR;

namespace Application.WorkTasks.Get;

public record GetWorkTasksQuery(string? SearchTerm, string? SortColumn, string? SortOrder, int Page, int PageSize) : IRequest<PagedList<WorkTaskResponse>>;