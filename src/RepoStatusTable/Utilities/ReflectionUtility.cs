using System;
using System.Linq;

namespace RepoStatusTable.Utilities;

public static class ReflectionUtility
{
	public static TOut GetValueOfProperty<TIn, TOut>( string propertyName, object? obj )
	{
		var allTableBorderProps = typeof(TIn).GetProperties();
		var requiredProp = allTableBorderProps.FirstOrDefault( s =>
			string.Equals( s.Name, propertyName, StringComparison.CurrentCultureIgnoreCase ) );

		if ( requiredProp?.GetValue( obj ) is TOut propVal )
		{
			return propVal;
		}

		throw new ArgumentException(
			$"Could not determine value for property '{propertyName}' from '{typeof(TIn).FullName}'" );
	}
}