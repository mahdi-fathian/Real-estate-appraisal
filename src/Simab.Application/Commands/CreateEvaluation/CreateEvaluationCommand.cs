using MediatR;
using Simab.Application.Common.Dtos;

namespace Simab.Application.Commands.CreateEvaluation;

/// <summary>
/// Command to create a new evaluation
/// </summary>
public record CreateEvaluationCommand(
    Guid PropertyId,
    Guid EvaluatorId,
    EvaluationTypeDto Type,
    decimal EstimatedAmount,
    string Currency = "IRR",
    Guid? CustomerId = null) : IRequest<Guid>;
