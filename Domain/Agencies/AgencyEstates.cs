using Fundalyzer.Domain.Estates;

namespace Fundalyzer.Domain.Agencies;

[GenerateListValueObject<Estate>]
public readonly partial record struct AgencyEstates
{
	public const string ErrorCode_Fundalyzer_AgencyEstates_Null = nameof(ErrorCode_Fundalyzer_AgencyEstates_Null);
}