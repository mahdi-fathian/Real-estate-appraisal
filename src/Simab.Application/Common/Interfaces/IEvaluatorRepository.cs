using Simab.Domain.Entities;

namespace Simab.Application.Common.Interfaces;

/// <summary>
/// Repository interface for Evaluator entity
/// </summary>
public interface IEvaluatorRepository : IRepository<Evaluator>
{
    Task<Evaluator?> GetByNationalIdAsync(string nationalId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Evaluator>> GetActiveEvaluatorsAsync(CancellationToken cancellationToken = default);
}
