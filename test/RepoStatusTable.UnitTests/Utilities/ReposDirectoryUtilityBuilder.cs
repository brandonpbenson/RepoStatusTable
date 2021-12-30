using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using RepoStatusTable.Facade;
using RepoStatusTable.Options;
using RepoStatusTable.Utilities;

namespace RepoStatusTable.UnitTests.Utilities;

public class ReposDirectoryUtilityBuilder
{
	private readonly Mock<IFileSystemFacade> _fileSystemFacade = new(MockBehavior.Strict);

	private readonly RepoOptions _repoOptions = new()
	{
		RepoDirs = new List<string>(),
		RepoRoots = new List<string>()
	};

	private readonly Mock<IVcsFacade> _vscFacade = new(MockBehavior.Strict);

	public ReposDirectoryUtilityBuilder WithRepoOptionsRepoDir( string path )
	{
		_repoOptions.RepoDirs = _repoOptions.RepoDirs.Append( path ).ToList();
		return this;
	}

	public ReposDirectoryUtilityBuilder WithRepoOptionsRepoRoot( string path )
	{
		_repoOptions.RepoRoots = _repoOptions.RepoRoots.Append( path ).ToList();
		return this;
	}

	public ReposDirectoryUtilityBuilder WithFileSystemFacadeGetFullPathReturns( string path )
	{
		_fileSystemFacade.Setup( m => m.GetFullPath( path ) ).Returns( path );
		return this;
	}

	public ReposDirectoryUtilityBuilder WithFileSystemFacadeDirectoryExists( string path, bool exists )
	{
		_fileSystemFacade.Setup( m => m.DirectoryExists( path ) ).Returns( exists );
		return this;
	}

	public ReposDirectoryUtilityBuilder WithFileSystemFacadeGetDirectories( string path, IEnumerable<string> dirs )
	{
		_fileSystemFacade.Setup( m => m.GetDirectories( path ) ).Returns( dirs );
		return this;
	}

	public ReposDirectoryUtilityBuilder WithVscFacadeIsValid( string path, bool isValid )
	{
		_vscFacade.Setup( m => m.IsVcsRepo( path ) ).Returns( isValid );
		return this;
	}

	public IReposDirectoryUtility Build()
	{
		return new ReposDirectoryUtility( new OptionsWrapper<RepoOptions>( _repoOptions ),
			_fileSystemFacade.Object,
			_vscFacade.Object );
	}

	public void VerifyNoOtherCalls()
	{
		_fileSystemFacade.VerifyNoOtherCalls();
		_vscFacade.VerifyNoOtherCalls();
	}
}