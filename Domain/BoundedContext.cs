namespace Fundalyzer.Domain;

public sealed class BoundedContext : IBoundedContext
{
	public static string Name => nameof(Fundalyzer);
}