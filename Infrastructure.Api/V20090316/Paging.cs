namespace Fundalyzer.Infrastructure.Api.V20090316;

[SuppressMessage("ReSharper", "IdentifierTypo")]
[SuppressMessage("ReSharper", "StringLiteralTypo")]
public record Paging
{
    public long AantalPaginas { get; init; }
    public long HuidigePagina { get; init; }
    public Uri VolgendeUrl { get; init; }
    public Uri VorigeUrl { get; init; }
}