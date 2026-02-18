using MediatR;
using Simab.Application.Common.Interfaces;
using Simab.Application.Common.Dtos;
using Simab.Application.Queries.GetEvaluation;
using AutoMapper;

namespace Simab.Application.Queries.GetEvaluation;

/// <summary>
/// Handler for getting an evaluation
/// </summary>
public class GetEvaluationHandler : IRequestHandler<GetEvaluationQuery, EvaluationDto?>
{
    private readonly IEvaluationRepository _evaluationRepository;
    private readonly IMapper _mapper;
    
    public GetEvaluationHandler(
        IEvaluationRepository evaluationRepository,
        IMapper mapper)
    {
        _evaluationRepository = evaluationRepository;
        _mapper = mapper;
    }
    
    public async Task<EvaluationDto?> Handle(GetEvaluationQuery request, CancellationToken cancellationToken)
    {
        var evaluation = await _evaluationRepository.GetByIdAsync(request.EvaluationId, cancellationToken);
        
        if (evaluation == null)
            return null;
            
        return _mapper.Map<EvaluationDto>(evaluation);
    }
}
