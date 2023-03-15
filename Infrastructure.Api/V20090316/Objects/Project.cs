namespace Fundalyzer.Infrastructure.Api.V20090316.Objects;

[SuppressMessage("ReSharper", "IdentifierTypo")]
[SuppressMessage("ReSharper", "StringLiteralTypo")]
public record Project
{
    public required int? AantalKamersTotEnMet { get; init; }
    public required int? AantalKamersVan { get; init; }
    public required int? AantalKavels { get; init; }
    public required string Adres { get; init; }
    public required Uri FriendlyUrl { get; init; }
    public required DateTime? GewijzigdDatum { get; init; }
    public required long? GlobalId { get; init; }
    public required string HoofdFoto { get; init; }
    public required bool IndIpix { get; init; }
    public required bool IndPdf { get; init; }
    public required bool IndPlattegrond { get; init; }
    public required bool IndTop { get; init; }
    public required bool IndVideo { get; init; }
    public required Guid InternalId { get; init; }
    public required long? MaxWoonoppervlakte { get; init; }
    public required long? MinWoonoppervlakte { get; init; }
    public required string Naam { get; init; }
    public required string Omschrijving { get; init; }
    public required List<object> OpenHuizen { get; init; }
    public required string Plaats { get; init; }
    public required long? Prijs { get; init; }
    public required string PrijsGeformatteerd { get; init; }
    public required DateTime? PublicatieDatum { get; init; }
    public required long Type { get; init; }
    public required object Woningtypen { get; init; }
}