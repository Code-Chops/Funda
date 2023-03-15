namespace Fundalyzer.Infrastructure.Api.V20090316.Objects;

[SuppressMessage("ReSharper", "IdentifierTypo")]
[SuppressMessage("ReSharper", "StringLiteralTypo")]
public record PromoLabel
{
    public required bool HasPromotionLabel { get; init; }
    public required List<Uri> PromotionPhotos { get; init; }
    public required List<string> PromotionPhotosSecure { get; init; }
    public required long PromotionType { get; init; }
    public required long RibbonColor { get; init; }
    public required string RibbonText { get; init; }
    public required string Tagline { get; init; }
}