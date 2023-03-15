namespace Fundalyzer.Domain.Estates;

/// <summary>
/// The place of residence of the real estate. It's called city here, but it also could be a village or town.
/// </summary>
[GenerateStringValueObject(
	minimumLength: 1, 
	maximumLength: 50, 
	useRegex: false, 
	stringFormat: StringFormat.Default, 
	stringComparison: StringComparison.OrdinalIgnoreCase)]
public readonly partial record struct EstateCity
{
	public const string ErrorCode_Fundalyzer_EstateCity_Null				= nameof(ErrorCode_Fundalyzer_EstateCity_Null);
	public const string ErrorCode_Fundalyzer_EstateCity_LengthOutOfRange	= nameof(ErrorCode_Fundalyzer_EstateCity_LengthOutOfRange);
}