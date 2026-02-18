using FluentValidation;
using Simab.Application.Commands.CreateEvaluation;

namespace Simab.Application.Commands.CreateEvaluation;

/// <summary>
/// Validator for CreateEvaluationCommand
/// </summary>
public class CreateEvaluationValidator : AbstractValidator<CreateEvaluationCommand>
{
    public CreateEvaluationValidator()
    {
        RuleFor(x => x.PropertyId)
            .NotEmpty().WithMessage("Property ID is required");
            
        RuleFor(x => x.EvaluatorId)
            .NotEmpty().WithMessage("Evaluator ID is required");
            
        RuleFor(x => x.EstimatedAmount)
            .GreaterThan(0).WithMessage("Estimated amount must be greater than zero");
            
        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Currency is required")
            .Length(3).WithMessage("Currency must be 3 characters");
    }
}
