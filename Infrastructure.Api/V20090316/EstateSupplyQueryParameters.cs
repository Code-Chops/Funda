using CodeChops.MagicEnums;
using Fundalyzer.Infrastructure.Api.V20090316.Objects;

namespace Fundalyzer.Infrastructure.Api.V20090316;

/// <summary>
/// Reflects the query parameters (enum member name) and their representation in a query string.  
/// </summary>
// This enum also ensures that renaming a query parameter does not result in an incorrect query string representation.
[SuppressMessage("ReSharper", "CommentTypo")]
public record EstateSupplyQueryParameter : MagicStringEnum<EstateSupplyQueryParameter>
{
	/// <summary>
	/// Query (zoekopdracht).
	/// </summary>
	public static readonly EstateSupplyQueryParameter Query		= CreateMember("zo");
		
	/// <summary>
	/// Supply type (<see cref="SoortAanbod"/>).
	/// </summary>
	public static readonly EstateSupplyQueryParameter Type		= CreateMember("type");

	public static readonly EstateSupplyQueryParameter Page		= CreateMember("page");
	public static readonly EstateSupplyQueryParameter PageSize	= CreateMember("pagesize");
}