using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using RepoStatusTable.Facade;

namespace RepoStatusTable.Options.Validation
{
	public class RepoOptionsValidator : IValidateOptions<RepoOptions>
	{
		private readonly IFileSystemFacade _fileSystemFacade;
		private readonly IVcsFacade _vcsFacade;

		public RepoOptionsValidator( IVcsFacade vcsFacade, IFileSystemFacade fileSystemFacade )
		{
			_vcsFacade = vcsFacade;
			_fileSystemFacade = fileSystemFacade;
		}

		public ValidateOptionsResult Validate( string name, RepoOptions options )
		{
			var errors = new List<string>();
			errors.AddRange( ValidateRepoDirs( options.RepoDirs ) );
			errors.AddRange( ValidateReposRoot( options.ReposRoot ) );

			return errors.Count != 0
				? ValidateOptionsResult.Fail( errors )
				: ValidateOptionsResult.Success;
		}

		private IEnumerable<string> ValidateRepoDirs( IEnumerable<string> paths )
		{
			return paths
				.Where( path => !_vcsFacade.IsVcsRepo( path ) )
				.Select( path => $"No VCS repository found in path {path}" );
		}

		private IEnumerable<string> ValidateReposRoot( IEnumerable<string> paths )
		{
			return paths
				.Where( path => !_fileSystemFacade.Exists( path ) )
				.Select( path => $"Directory {path} does not exist" );
		}
	}
}