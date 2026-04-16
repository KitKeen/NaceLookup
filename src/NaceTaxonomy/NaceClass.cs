namespace Nace;

/// <summary>Represents a NACE Rev. 2.1 class (4-digit level).</summary>
public sealed record NaceClass(
    string Code,
    string Description,
    string Section,
    string SectionName);
