using Simab.Domain.Entities;
using Simab.Domain.Enums;

namespace Simab.Application.Common.Interfaces;

/// <summary>
/// Repository interface for Workflow entity
/// </summary>
public interface IWorkflowRepository : IRepository<Workflow>
{
    Task<IEnumerable<Workflow>> GetByEvaluatorIdAsync(Guid evaluatorId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Workflow>> GetByStatusAsync(WorkflowStatus status, CancellationToken cancellationToken = default);
    Task<Workflow?> GetByEvaluationIdAsync(Guid evaluationId, CancellationToken cancellationToken = default);
}
