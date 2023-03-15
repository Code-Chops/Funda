namespace Fundalyzer.Infrastructure.Api.V20090316.Objects;

[SuppressMessage("ReSharper", "IdentifierTypo")]
[SuppressMessage("ReSharper", "StringLiteralTypo")]
[Flags]
public enum Faciliteiten : byte
{
	Tuin	= 1 << 0,
	Zwembad = 1 << 1,
}