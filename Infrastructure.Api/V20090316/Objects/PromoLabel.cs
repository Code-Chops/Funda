// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable CS8618

namespace Fundalyzer.Infrastructure.Api.V20090316.Objects;

[SuppressMessage("ReSharper", "IdentifierTypo")]
[SuppressMessage("ReSharper", "StringLiteralTypo")]
public record PromoLabel
{
    public bool HasPromotionLabel { get; init; }
    public List<Uri> PromotionPhotos { get; init; }
    public List<string> PromotionPhotosSecure { get; init; }
    public long PromotionType { get; init; }
    public long RibbonColor { get; init; }
    public string RibbonText { get; init; }
    public string Tagline { get; init; }
}