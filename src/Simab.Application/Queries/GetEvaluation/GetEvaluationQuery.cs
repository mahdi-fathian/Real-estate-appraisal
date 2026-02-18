using MediatR;
using Simab.Application.Common.Dtos;

namespace Simab.Application.Queries.GetEvaluation;

/// <summary>
/// Query to get an evaluation by ID
/// </summary>
public record GetEvaluationQuery(Guid EvaluationId) : IRequest<EvaluationDto?>;
