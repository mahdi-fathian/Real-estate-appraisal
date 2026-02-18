using Simab.Domain.Common;
using Simab.Domain.Enums;

namespace Simab.Domain.Entities;

/// <summary>
/// Represents a document attached to an evaluation
/// </summary>
public class Document : Entity
{
    public Guid EvaluationId { get; private set; }
    public DocumentType Type { get; private set; }
    public string FileName { get; private set; } = null!;
    public string FilePath { get; private set; } = null!;
    public long FileSize { get; private set; }
    public string? MimeType { get; private set; }
    public string? Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    private Document() { }
    
    public Document(
        Guid evaluationId,
        DocumentType type,
        string fileName,
        string filePath,
        long fileSize,
        string? mimeType = null,
        string? description = null)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("File name cannot be null or empty", nameof(fileName));
            
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be null or empty", nameof(filePath));
            
        if (fileSize <= 0)
            throw new ArgumentException("File size must be greater than zero", nameof(fileSize));
            
        EvaluationId = evaluationId;
        Type = type;
        FileName = fileName;
        FilePath = filePath;
        FileSize = fileSize;
        MimeType = mimeType;
        Description = description;
        CreatedAt = DateTime.UtcNow;
    }
    
    public void UpdateDescription(string? description)
    {
        Description = description;
    }
}
