using Domain.Data;
using Domain.WorkTasks;
using MediatR;

namespace Application.WorkTasks.Update;

internal sealed class UpdateWorkTaskCommandHandler : IRequestHandler<UpdateWorkTaskCommand>
{
    private readonly IWorkTaskRepository _workTaskRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateWorkTaskCommandHandler(IWorkTaskRepository workTaskRepository, IUnitOfWork unitOfWork)
    {
        _workTaskRepository = workTaskRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateWorkTaskCommand request, CancellationToken cancellationToken)
    {
        var workTask = await _workTaskRepository.GetByIdAsync(request.WorkTaskId);

        if (workTask == null)
        {
            throw new WorkTaskNotFoundException(request.WorkTaskId);
        }

        workTask.Update(request.Description, request.CategoryL1Id, request.RunTime);

        _workTaskRepository.Update(workTask);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
