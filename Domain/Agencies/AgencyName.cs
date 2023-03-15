namespace Fundalyzer.Domain.Agencies;

[GenerateStringValueObject(
	minimumLength: 1, 
	maximumLength: 50, 
	useRegex: false, 
	stringFormat: StringFormat.Default, 
	stringComparison: StringComparison.OrdinalIgnoreCase)]
public readonly partial record struct AgencyName
{
	public const string ErrorCode_Fundalyzer_AgencyName_Null				= nameof(ErrorCode_Fundalyzer_AgencyName_Null);
	public const string ErrorCode_Fundalyzer_AgencyName_LengthOutOfRange	= nameof(ErrorCode_Fundalyzer_AgencyName_LengthOutOfRange);
}