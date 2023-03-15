namespace Fundalyzer.Infrastructure.Api.V20090316.Objects;

[SuppressMessage("ReSharper", "IdentifierTypo")]
[SuppressMessage("ReSharper", "StringLiteralTypo")]
public record Metadata
{
    public required string ObjectType { get; init; }
    public required string Omschrijving { get; init; }
    public required string Titel { get; init; }
}