namespace Nace;

/// <summary>Represents a NACE Rev. 2.1 class (4-digit level).</summary>
public sealed class NaceClass
{
    public string Code { get; }
    public string Description { get; }
    public string Section { get; }
    public string SectionName { get; }

    internal NaceClass(string code, string description, string section, string sectionName)
    {
        Code = code;
        Description = description;
        Section = section;
        SectionName = sectionName;
    }
}
