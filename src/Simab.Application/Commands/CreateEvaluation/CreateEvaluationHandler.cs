using MediatR;
using Simab.Application.Common.Interfaces;
using Simab.Application.Common.Dtos;
using Simab.Domain.ValueObjects;
using Simab.Application.Commands.CreateEvaluation;

namespace Simab.Application.Commands.CreateEvaluation;

/// <summary>
/// Handler for creating a new evaluation
/// </summary>
public class CreateEvaluationHandler : IRequestHandler<CreateEvaluationCommand, Guid>
{
    private readonly IEvaluationRepository _evaluationRepository;
    private readonly IPropertyRepository _propertyRepository;
    private readonly IEvaluatorRepository _evaluatorRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateEvaluationHandler(
        IEvaluationRepository evaluationRepository,
        IPropertyRepository propertyRepository,
        IEvaluatorRepository evaluatorRepository,
        IUnitOfWork unitOfWork)
    {
        _evaluationRepository = evaluationRepository;
        _propertyRepository = propertyRepository;
        _evaluatorRepository = evaluatorRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Guid> Handle(CreateEvaluationCommand request, CancellationToken cancellationToken)
    {
        // Verify property exists
        var property = await _propertyRepository.GetByIdAsync(request.PropertyId, cancellationToken);
        if (property == null)
            throw new InvalidOperationException($"Property with ID {request.PropertyId} not found");
            
        // Verify evaluator exists and is active
        var evaluator = await _evaluatorRepository.GetByIdAsync(request.EvaluatorId, cancellationToken);
        if (evaluator == null)
            throw new InvalidOperationException($"Evaluator with ID {request.EvaluatorId} not found");
            
        if (!evaluator.IsActive)
            throw new InvalidOperationException($"Evaluator with ID {request.EvaluatorId} is not active");
            
        // Create money value object
        var estimatedValue = new Money(request.EstimatedAmount, request.Currency);
        
        // Map evaluation type
        var evaluationType = request.Type switch
        {
            EvaluationTypeDto.Standard => Domain.Enums.EvaluationType.Standard,
            EvaluationTypeDto.Detailed => Domain.Enums.EvaluationType.Detailed,
            EvaluationTypeDto.Quick => Domain.Enums.EvaluationType.Quick,
            EvaluationTypeDto.Comprehensive => Domain.Enums.EvaluationType.Comprehensive,
            _ => Domain.Enums.EvaluationType.Standard
        };
        
        var evaluation = new Domain.Entities.Evaluation(
            request.PropertyId,
            request.EvaluatorId,
            evaluationType,
            estimatedValue,
            request.CustomerId);
            
        await _evaluationRepository.AddAsync(evaluation, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return evaluation.Id;
    }
}
