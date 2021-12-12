using Microsoft.Extensions.Options;
using RepoStatusTable.Options.CellProvider;

namespace RepoStatusTable.Options.Validation;

public class FileContentProviderOptionsValidator : IValidateOptions<FileContentProviderOptions>
{
	public ValidateOptionsResult Validate( string name, FileContentProviderOptions options )
	{
		if ( options.Enable == false )
		{
			return ValidateOptionsResult.Skip;
		}

		if ( string.IsNullOrWhiteSpace( options.Path ) )
		{
			return ValidateOptionsResult.Fail( "File content provider requires a configured file path" );
		}

		return ValidateOptionsResult.Success;
	}
}