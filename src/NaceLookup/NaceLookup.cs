using System.Collections.Generic;
using System.Linq;

namespace Nace;

/// <summary>
/// O(1) lookup for NACE Rev. 2.1 class codes (651 classes, sections A–V).
/// Thread-safe. Zero allocation after initialization.
/// </summary>
public static class NaceLookup
{
    private static readonly IReadOnlyDictionary<string, NaceClass> _index = Build();

    private static IReadOnlyDictionary<string, NaceClass> Build()
    {
        var dict = new Dictionary<string, NaceClass>(NaceData.Classes.Length, StringComparer.OrdinalIgnoreCase);
        foreach (var (code, desc, section, sectionName) in NaceData.Classes)
            dict[code] = new NaceClass(code, desc, section, sectionName);
        return dict;
    }

    /// <summary>Total number of NACE classes in the taxonomy.</summary>
    public static int Count => _index.Count;

    /// <summary>
    /// Returns the <see cref="NaceClass"/> for the given code, or <c>null</c> if not found.
    /// Accepts formats: "01.11", "0111", "1.11".
    /// </summary>
    public static NaceClass? Get(string code)
    {
        if (string.IsNullOrWhiteSpace(code)) return null;
        return _index.TryGetValue(Normalize(code), out var result) ? result : null;
    }

    /// <summary>Returns true if the code exists in the taxonomy.</summary>
    public static bool TryGet(string code, out NaceClass? naceClass)
    {
        naceClass = Get(code);
        return naceClass is not null;
    }

    /// <summary>Returns all classes in a given section (e.g. "K").</summary>
    public static IEnumerable<NaceClass> GetBySection(string section) =>
        _index.Values.Where(c => string.Equals(c.Section, section, StringComparison.OrdinalIgnoreCase));

    /// <summary>Returns all NACE classes.</summary>
    public static IEnumerable<NaceClass> All => _index.Values;

    private static string Normalize(string code)
    {
        var s = code.Trim();
        if (s.Length == 4 && !s.Contains('.'))
            s = s.Substring(0, 2) + "." + s.Substring(2);
        if (s.Length == 4 && s[1] == '.')
            s = "0" + s;
        return s;
    }
}
