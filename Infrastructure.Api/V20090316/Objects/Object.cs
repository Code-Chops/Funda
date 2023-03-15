// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable CS8618

namespace Fundalyzer.Infrastructure.Api.V20090316.Objects;

[SuppressMessage("ReSharper", "IdentifierTypo")]
[SuppressMessage("ReSharper", "StringLiteralTypo")]
public record Object
{
    public string AangebodenSindsTekst { get; init; }
    public long? AantalBeschikbaar { get; init; }
    public long? AantalKamers { get; init; }
    public long? AantalKavels { get; init; }
    public string Adres { get; init; }
    public long Afstand { get; init; }
    //public BronCode BronCode { get; init; }
    public List<object> ChildrenObjects { get; init; }
    public DateTime? DatumAanvaarding { get; init; }
    public DateTime? DatumOndertekeningAkte { get; init; }
    public Uri Foto { get; init; }
    public Uri FotoLarge { get; init; }
    public Uri FotoLargest { get; init; }
    public Uri FotoMedium { get; init; }
    public Uri FotoSecure { get; init; }
    public DateTime? GewijzigdDatum { get; init; }
    public long? GlobalId { get; init; }
    public Guid GroupByObjectType { get; init; }
    public bool Heeft360GradenFoto { get; init; }
    public bool HeeftBrochure { get; init; }
    public bool HeeftOpenhuizenTopper { get; init; }
    public bool HeeftOverbruggingsgrarantie { get; init; }
    public bool HeeftPlattegrond { get; init; }
    public bool HeeftTophuis { get; init; }
    public bool HeeftVeiling { get; init; }
    public bool HeeftVideo { get; init; }
    public long? HuurPrijsTot { get; init; }
    public long? Huurprijs { get; init; }
    public string HuurprijsFormaat { get; init; }
    public Guid Id { get; init; }
    public long? InUnitsVanaf { get; init; }
    public bool IndProjectObjectType { get; init; }
    public bool? IndTransactieMakelaarTonen { get; init; }
    public bool IsSearchable { get; init; }
    public bool IsVerhuurd { get; init; }
    public bool IsVerkocht { get; init; }
    public bool IsVerkochtOfVerhuurd { get; init; }
    public long? Koopprijs { get; init; }
    public string KoopprijsFormaat { get; init; }
    public long? KoopprijsTot { get; init; }
    public Land? Land { get; init; }
    public long MakelaarId { get; init; }
    public string MakelaarNaam { get; init; }
    public Uri MobileUrl { get; init; }
    public string Note { get; init; }
    public List<object> OpenHuis { get; init; }
    public long Oppervlakte { get; init; }
    public long? Perceeloppervlakte { get; init; }
    public string Postcode { get; init; }
    public Prijs Prijs { get; init; }
    public string PrijsGeformatteerdHtml { get; init; }
    public string PrijsGeformatteerdTextHuur { get; init; }
    public string PrijsGeformatteerdTextKoop { get; init; }
    public Project Project { get; init; }
    public string ProjectNaam { get; init; }
    public PromoLabel PromoLabel { get; init; }
    public string PublicatieDatum { get; init; }
    public long PublicatieStatus { get; init; }
    public DateTime? SavedDate { get; init; }
    public SoortAanbod SoortAanbod { get; init; }
    public long ObjectSoortAanbod { get; init; }
    public string StartOplevering { get; init; }
    public string TimeAgoText { get; init; }
    public DateTime? TransactieAfmeldDatum { get; init; }
    public string TransactieMakelaarId { get; init; }
    public string TransactieMakelaarNaam { get; init; }
    public long TypeProject { get; init; }
    public Uri Url { get; init; }
    public double Wgs84X { get; init; }
    public double Wgs84Y { get; init; }
    public long? WoonOppervlakteTot { get; init; }
    public long? Woonoppervlakte { get; init; }
    public string Woonplaats { get; init; }
    public List<long> ZoekType { get; init; }
}