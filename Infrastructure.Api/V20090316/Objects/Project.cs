// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable CS8618

namespace Fundalyzer.Infrastructure.Api.V20090316.Objects;

[SuppressMessage("ReSharper", "IdentifierTypo")]
[SuppressMessage("ReSharper", "StringLiteralTypo")]
public record Project
{
    public int? AantalKamersTotEnMet { get; init; }
    public int? AantalKamersVan { get; init; }
    public int? AantalKavels { get; init; }
    public string Adres { get; init; }
    public Uri FriendlyUrl { get; init; }
    public DateTime? GewijzigdDatum { get; init; }
    public long? GlobalId { get; init; }
    public string HoofdFoto { get; init; }
    public bool IndIpix { get; init; }
    public bool IndPdf { get; init; }
    public bool IndPlattegrond { get; init; }
    public bool IndTop { get; init; }
    public bool IndVideo { get; init; }
    public Guid InternalId { get; init; }
    public long? MaxWoonoppervlakte { get; init; }
    public long? MinWoonoppervlakte { get; init; }
    public string Naam { get; init; }
    public string Omschrijving { get; init; }
    public List<object> OpenHuizen { get; init; }
    public string Plaats { get; init; }
    public long? Prijs { get; init; }
    public string PrijsGeformatteerd { get; init; }
    public DateTime? PublicatieDatum { get; init; }
    public long Type { get; init; }
    public object Woningtypen { get; init; }
}