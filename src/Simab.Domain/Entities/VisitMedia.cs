using Simab.Domain.Common;
using Simab.Domain.Enums;

namespace Simab.Domain.Entities;

/// <summary>
/// Represents media (photo/video/audio) attached to a visit
/// </summary>
public class VisitMedia : Entity
{
    public Guid VisitId { get; private set; }
    public MediaType Type { get; private set; }
    public string FilePath { get; private set; } = null!;
    public string FileName { get; private set; } = null!;
    public long FileSize { get; private set; }
    public string? MimeType { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    private VisitMedia() { }
    
    public VisitMedia(
        Guid visitId,
        MediaType type,
        string filePath,
        string fileName,
        long fileSize,
        string? mimeType = null)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be null or empty", nameof(filePath));
            
        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("File name cannot be null or empty", nameof(fileName));
            
        if (fileSize <= 0)
            throw new ArgumentException("File size must be greater than zero", nameof(fileSize));
            
        VisitId = visitId;
        Type = type;
        FilePath = filePath;
        FileName = fileName;
        FileSize = fileSize;
        MimeType = mimeType;
        CreatedAt = DateTime.UtcNow;
    }
}
