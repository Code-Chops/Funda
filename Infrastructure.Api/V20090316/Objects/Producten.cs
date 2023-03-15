using System.Runtime.Serialization;

namespace Fundalyzer.Infrastructure.Api.V20090316.Objects;

[SuppressMessage("ReSharper", "IdentifierTypo")]
[SuppressMessage("ReSharper", "StringLiteralTypo")]
public enum Producten
{
	Brochure, 
	Featured, 
	Plattegrond,
	[EnumMember(Value = "360-fotos")]
	The360Fotos, 
	Toppositie, 
	Video,
	Inbeeld,
};