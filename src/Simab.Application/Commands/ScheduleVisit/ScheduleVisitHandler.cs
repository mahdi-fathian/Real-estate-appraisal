using MediatR;
using Simab.Application.Common.Interfaces;
using Simab.Domain.Entities;
using Simab.Application.Commands.ScheduleVisit;

namespace Simab.Application.Commands.ScheduleVisit;

/// <summary>
/// Handler for scheduling a visit
/// </summary>
public class ScheduleVisitHandler : IRequestHandler<ScheduleVisitCommand, Guid>
{
    private readonly IEvaluationRepository _evaluationRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public ScheduleVisitHandler(
        IEvaluationRepository evaluationRepository,
        IUnitOfWork unitOfWork)
    {
        _evaluationRepository = evaluationRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Guid> Handle(ScheduleVisitCommand request, CancellationToken cancellationToken)
    {
        var evaluation = await _evaluationRepository.GetByIdAsync(request.EvaluationId, cancellationToken);
        if (evaluation == null)
            throw new InvalidOperationException($"Evaluation with ID {request.EvaluationId} not found");
            
        var visit = new Visit(
            request.EvaluationId,
            request.EvaluatorId,
            request.ScheduledDate);
            
        evaluation.AddVisit(visit);
        await _evaluationRepository.UpdateAsync(evaluation, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return visit.Id;
    }
}
