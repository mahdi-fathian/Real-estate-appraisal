using FluentValidation;
using Simab.Application.Commands.ScheduleVisit;

namespace Simab.Application.Commands.ScheduleVisit;

/// <summary>
/// Validator for ScheduleVisitCommand
/// </summary>
public class ScheduleVisitValidator : AbstractValidator<ScheduleVisitCommand>
{
    public ScheduleVisitValidator()
    {
        RuleFor(x => x.EvaluationId)
            .NotEmpty().WithMessage("Evaluation ID is required");
            
        RuleFor(x => x.EvaluatorId)
            .NotEmpty().WithMessage("Evaluator ID is required");
            
        RuleFor(x => x.ScheduledDate)
            .NotEmpty().WithMessage("Scheduled date is required")
            .Must(date => date > DateTime.UtcNow)
            .WithMessage("Scheduled date must be in the future");
    }
}
