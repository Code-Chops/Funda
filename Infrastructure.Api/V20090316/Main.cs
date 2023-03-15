using Fundalyzer.Infrastructure.Api.V20090316.Objects;
using Object = Fundalyzer.Infrastructure.Api.V20090316.Objects.Object;

namespace Fundalyzer.Infrastructure.Api.V20090316;

[SuppressMessage("ReSharper", "IdentifierTypo")]
[SuppressMessage("ReSharper", "StringLiteralTypo")]
public record Main
{
    public required long AccountStatus { get; init; }
    public required bool EmailNotConfirmed { get; init; }
    public required bool ValidationFailed { get; init; }
    public required object ValidationReport { get; init; }
    public required long Website { get; init; }
    public required Metadata Metadata { get; init; }
    public required List<Object> Objects { get; init; }
    public required Paging Paging { get; init; }
    public required long TotaalAantalObjecten { get; init; }
}