using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using RepoStatusTable.Facade;

namespace RepoStatusTable.IntegrationTests.FacadeMocks;

public class GitFacadeMockSetup
{
	private readonly GitFacadeMock _gitFacadeInstance;
	private readonly GitFacadeMock _vscFacadeInstance;

	public GitFacadeMockSetup( IServiceProvider provider )
	{
		_gitFacadeInstance = provider.GetRequiredService<IGitFacade>() as GitFacadeMock ??
		                     throw new InvalidOperationException();
		_vscFacadeInstance = provider.GetRequiredService<IVcsFacade>() as GitFacadeMock ??
		                     throw new InvalidOperationException();
	}

	public GitFacadeMockSetup IsVscRepoReturnsForPath( string path, bool returnValue )
	{
		_vscFacadeInstance.InternalMock.Setup( m => m.IsVcsRepo( path ) ).Returns( returnValue );
		return this;
	}

	public GitFacadeMockSetup GetStatusReturnsForPath( string path, int added, int modified, int missing,
		int untracked )
	{
		var dict = new Dictionary<string, int>
		{
			{ "Added", added },
			{ "Modified", modified },
			{ "Missing", missing },
			{ "Untracked", untracked }
		};
		_gitFacadeInstance.InternalMock.Setup( m => m.GetStatus( path ) ).Returns( dict );
		return this;
	}

	public GitFacadeMockSetup GetBranchReturnsForPath( string path, string branch )
	{
		_gitFacadeInstance.InternalMock.Setup( m => m.GetCurrentBranch( path ) ).Returns( branch );
		return this;
	}
}