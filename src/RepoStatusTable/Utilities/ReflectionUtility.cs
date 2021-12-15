namespace RepoStatusTable.Utilities;

public static class ReflectionUtility
{
	public static T GetValueOfProperty<T>( string propertyName, object? obj )
	{
		var allProps = typeof(T).GetProperties();
		var requiredProp = allProps.FirstOrDefault( s =>
			string.Equals( s.Name, propertyName, StringComparison.CurrentCultureIgnoreCase ) );

		if ( requiredProp?.GetValue( obj ) is T propVal )
		{
			return propVal;
		}

		throw new ArgumentException(
			$"Could not determine value for property '{propertyName}' from '{typeof(T).FullName}'" );
	}
}