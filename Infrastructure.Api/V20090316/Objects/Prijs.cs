namespace Fundalyzer.Infrastructure.Api.V20090316.Objects;

[SuppressMessage("ReSharper", "IdentifierTypo")]
[SuppressMessage("ReSharper", "StringLiteralTypo")]
public record Prijs
{
    public required bool GeenExtraKosten { get; init; }
    public required string HuurAbbreviation { get; init; }
    public required long? Huurprijs { get; init; }
    public required string HuurprijsOpAanvraag { get; init; }
    public required long? HuurprijsTot { get; init; }
    public required string KoopAbbreviation { get; init; }
    public required long? Koopprijs { get; init; }
    public required string KoopprijsOpAanvraag { get; init; }
    public required long? KoopprijsTot { get; init; }
    public required long? OriginelePrijs { get; init; }
    public required string VeilingText { get; init; }
}