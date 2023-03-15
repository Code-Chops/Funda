using CodeChops.MagicEnums;

namespace Fundalyzer.Domain.Estates;

public record EstateFacilities : MagicFlagsEnum<EstateFacilities>
{
	public static readonly EstateFacilities Garden			= CreateMember(1 << 0);
	public static readonly EstateFacilities SwimmingPool	= CreateMember(1 << 1);
} 