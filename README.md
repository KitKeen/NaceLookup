# NaceLookup

O(1) NACE Rev. 2.1 code lookup for .NET.

Maps 651 class codes (sections A–V) to human-readable descriptions and section names. Backed by a dictionary indexed by NACE code — thread-safe, zero-allocation after initialization, no external dependencies.

## Install

```bash
dotnet add package NaceLookup
```

## Usage

```csharp
using Nace;

// Basic lookup
var result = NaceLookup.Get("64.19");
// result.Code        → "64.19"
// result.Description → "Other monetary intermediation"
// result.Section     → "L"
// result.SectionName → "Financial and Insurance Activities"

// Try-pattern
if (NaceLookup.TryGet("62.10", out var nace))
    Console.WriteLine(nace.Description); // "Computer programming activities"

// Get all codes in a section
var fintech = NaceLookup.GetBySection("L");

// Returns null for unknown codes, never throws
var unknown = NaceLookup.Get("99.99"); // null

// Flexible input formats
NaceLookup.Get("6419");  // same as "64.19"
NaceLookup.Get("01.11"); // standard format

// Total number of codes
Console.WriteLine(NaceLookup.Count); // 651
```

## API

| Method | Description |
|--------|-------------|
| `NaceLookup.Get(string code)` | Returns `NaceClass` or `null` |
| `NaceLookup.TryGet(string code, out NaceClass?)` | Try-pattern lookup |
| `NaceLookup.GetBySection(string section)` | All classes in a section (e.g. `"L"`) |
| `NaceLookup.All` | All 651 classes |
| `NaceLookup.Count` | Total number of classes (651) |

`NaceClass` record:

| Property | Type | Example |
|----------|------|---------|
| `Code` | `string` | `"64.19"` |
| `Description` | `string` | `"Other monetary intermediation"` |
| `Section` | `string` | `"L"` |
| `SectionName` | `string` | `"Financial and Insurance Activities"` |

## Sections

| Section | Name |
|---------|------|
| A | Agriculture, Forestry and Fishing |
| B | Mining and Quarrying |
| C | Manufacturing |
| D | Electricity, Gas, Steam and Air Conditioning Supply |
| E | Water Supply, Sewerage, Waste Management and Remediation |
| F | Construction |
| G | Wholesale and Retail Trade |
| H | Transportation and Storage |
| I | Accommodation and Food Service Activities |
| J | Publishing, Broadcasting and Content Production and Distribution |
| K | Telecommunication, Computer Programming, Consulting and Computing Infrastructure |
| L | Financial and Insurance Activities |
| M | Real Estate Activities |
| N | Professional, Scientific and Technical Activities |
| O | Administrative and Support Service Activities |
| P | Public Administration and Defence; Compulsory Social Security |
| Q | Education |
| R | Human Health and Social Work Activities |
| S | Arts, Sports and Recreation |
| T | Other Service Activities |
| U | Activities of Households as Employers |
| V | Activities of Extraterritorial Organisations and Bodies |

## Data source

The classification data is taken from:

> **NACE Rev. 2.1 – Statistical classification of economic activities in the European Union**, 2025 edition.
> Published by Eurostat, European Commission.
> ISBN 978-92-68-13139-8 · doi:10.2785/155339
> Licensed under [Creative Commons Attribution 4.0 International (CC BY 4.0)](https://creativecommons.org/licenses/by/4.0/).
> Source: [ec.europa.eu/eurostat](https://ec.europa.eu/eurostat)

This package covers all 651 four-digit classes from NACE Rev. 2.1 (the most recent revision as of 2025).

## License

MIT
