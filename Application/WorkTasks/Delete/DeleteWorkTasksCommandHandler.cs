using Domain.Data;
using Domain.WorkTasks;
using MediatR;

namespace Application.WorkTasks.Delete;

internal sealed class DeleteWorkTasksCommandHandler : IRequestHandler<DeleteWorkTasksCommand>
{
    private readonly IWorkTaskRepository _workTaskRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteWorkTasksCommandHandler(IWorkTaskRepository workTaskRepository, IUnitOfWork unitOfWork)
    {
        _workTaskRepository = workTaskRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteWorkTasksCommand request, CancellationToken cancellationToken)
    {
        var workTask = await _workTaskRepository.GetByIdAsync(request.WorkTaskId);

        if (workTask == null)
        {
            throw new WorkTaskNotFoundException(request.WorkTaskId);
        }

        _workTaskRepository.Remove(workTask);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
