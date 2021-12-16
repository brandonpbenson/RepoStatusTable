using RepoStatusTable.Utilities;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace RepoStatusTable.Options.Validation;

/// <summary>
///     Specifies that a data field value is required
///     if the class contains a boolean property "Enable" that is set to true.
/// </summary>
/// <remarks>
///     Validation successes if either the "Enable" attribute is set to false
///     or the annotated property/field is not null and not whitespace (in case of a string).
///     Otherwise, validation fails.
/// </remarks>
/// <remarks>
///     The surrounding class needs to contain a boolean property "Enable".
/// </remarks>
/// <exception cref="ArgumentException">
///     If the surrounding class does not contain a boolean property "Enable".
/// </exception>
[AttributeUsage( AttributeTargets.Property | AttributeTargets.Field )]
public class RequiredIfEnabledAttribute : ValidationAttribute
{
	private const string BooleanPropertyName = "Enable";

	protected override ValidationResult? IsValid( object? value, ValidationContext context )
	{
		var type = context.ObjectType;
		var obj = context.ObjectInstance;

		switch ( IsEnabled( type, obj ) )
		{
			case false:
			case true when IsValueValid( value ):
				return ValidationResult.Success;
			default:
				return new ValidationResult( $"{type.FullName} is enabled, " +
				                             $"but {context.DisplayName} is not set",
					new[] { context.DisplayName } );
		}
	}

	private static bool IsValueValid( object? value )
	{
		if ( value is string valueString )
		{
			return !string.IsNullOrWhiteSpace( valueString );
		}

		return value is not null;
	}

	private static bool IsEnabled( Type type, object instance )
	{
		return ReflectionUtility.GetValueOfProperty<bool>( type, instance, BooleanPropertyName );
	}
}