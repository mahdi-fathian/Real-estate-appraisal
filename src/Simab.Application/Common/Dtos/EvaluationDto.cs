namespace Simab.Application.Common.Dtos;

/// <summary>
/// DTO for Evaluation entity
/// </summary>
public record EvaluationDto
{
    public Guid Id { get; init; }
    public Guid PropertyId { get; init; }
    public Guid EvaluatorId { get; init; }
    public Guid? CustomerId { get; init; }
    public string Status { get; init; } = null!;
    public string Type { get; init; } = null!;
    public decimal EstimatedAmount { get; init; }
    public string Currency { get; init; } = null!;
    public DateTime CreatedAt { get; init; }
    public DateTime? CompletedAt { get; init; }
    public string? Notes { get; init; }
    public bool IsEncrypted { get; init; }
    public bool HasDigitalSignature { get; init; }
    public int DocumentCount { get; init; }
    public int VisitCount { get; init; }
}
