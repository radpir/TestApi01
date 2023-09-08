using Domain.Data;
using Domain.WorkTasks;
using MediatR;

namespace Application.WorkTasks.Create;

internal sealed class CreateWorkTaskCommandHandler : IRequestHandler<CreateWorkTaskCommand>
{
    private readonly IWorkTaskRepository _workTaskRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateWorkTaskCommandHandler(IWorkTaskRepository workTaskRepository, IUnitOfWork unitOfWork)
    {
        _workTaskRepository = workTaskRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateWorkTaskCommand request, CancellationToken cancellationToken)
    {
        var workTask = WorkTask.Create(request.Description, request.CategoryL1Id);

        _workTaskRepository.Add(workTask);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
