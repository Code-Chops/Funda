// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable CS8618

namespace Fundalyzer.Infrastructure.Api.V20090316.Objects;

[SuppressMessage("ReSharper", "IdentifierTypo")]
[SuppressMessage("ReSharper", "StringLiteralTypo")]
public record Prijs
{
    public bool GeenExtraKosten { get; init; }
    public string HuurAbbreviation { get; init; }
    public long? Huurprijs { get; init; }
    public string HuurprijsOpAanvraag { get; init; }
    public long? HuurprijsTot { get; init; }
    public string KoopAbbreviation { get; init; }
    public long? Koopprijs { get; init; }
    public string KoopprijsOpAanvraag { get; init; }
    public long? KoopprijsTot { get; init; }
    public long? OriginelePrijs { get; init; }
    public string VeilingText { get; init; }
}