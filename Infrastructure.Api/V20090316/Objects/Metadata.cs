// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable CS8618

namespace Fundalyzer.Infrastructure.Api.V20090316.Objects;

[SuppressMessage("ReSharper", "IdentifierTypo")]
[SuppressMessage("ReSharper", "StringLiteralTypo")]
public record Metadata
{
    public string ObjectType { get; init; }
    public string Omschrijving { get; init; }
    public string Titel { get; init; }
}