using System.ComponentModel.DataAnnotations;

namespace Fundalyzer.Infrastructure.Api;

/// <summary>
/// Settings which are retrieved from appsettings.json.
/// </summary>
public sealed record Settings
{
	public const string SectionName = "Settings"; // Don't use nameof() because refactoring could change the name and that would lead to unexpected behaviour.
	
	/// <summary>
	/// The URL of the Funda JSON REST API.
	/// </summary>
	[Url]
	[Required]
	public required string ApiUrl { get; init; }

	/// <summary>
	/// The key of the Funda API.
	/// </summary>
	[RegularExpression(@"^[{]?[0-9a-fA-F]{32}", ErrorMessage = "API key should be a GUID")]
	[Required]
	public required string ApiKey { get; init; }

	/// <summary>
	/// The size of each page requested.
	/// </summary>
	[Range(0, 250, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
	[Required]
	public required int PageSize { get; init; }
	
	/// <summary>
	/// The delay (in milliseconds) between each request (so the server won't get spammed).
	/// </summary>
	[Range(30, 5000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
	[Required]
	public required int RequestDelayInMilliseconds { get; init; }
}