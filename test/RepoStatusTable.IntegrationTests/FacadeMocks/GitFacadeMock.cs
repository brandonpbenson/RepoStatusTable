using System.Collections.Generic;
using Moq;
using RepoStatusTable.Facade;

namespace RepoStatusTable.IntegrationTests.FacadeMocks;

public class GitFacadeMock : IGitFacade
{
	public readonly Mock<IGitFacade> InternalMock = new(MockBehavior.Strict);

	public bool IsVcsRepo( string path )
	{
		return InternalMock.Object.IsVcsRepo( path );
	}

	public string GetCurrentBranch( string path )
	{
		return InternalMock.Object.GetCurrentBranch( path );
	}

	public IDictionary<string, int> GetStatus( string path )
	{
		return InternalMock.Object.GetStatus( path );
	}
}