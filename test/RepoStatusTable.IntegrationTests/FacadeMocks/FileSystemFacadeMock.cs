using System;
using System.Collections.Generic;
using Moq;
using RepoStatusTable.Facade;

namespace RepoStatusTable.IntegrationTests.FacadeMocks;

public class FileSystemFacadeMock : IFileSystemFacade
{
	public readonly Mock<IFileSystemFacade> InternalMock = new(MockBehavior.Strict);

	public string GetFullPath( string path )
	{
		return InternalMock.Object.GetFullPath( path );
	}

	public IEnumerable<string> GetDirectories( string path )
	{
		return InternalMock.Object.GetDirectories( path );
	}

	public string GetDirectoryName( string path )
	{
		return InternalMock.Object.GetDirectoryName( path );
	}

	public string Join( string? path1, string? path2 )
	{
		return InternalMock.Object.Join( path1, path2 );
	}

	public bool DirectoryExists( string path )
	{
		return InternalMock.Object.DirectoryExists( path );
	}

	public IEnumerable<string> ReadText( string path )
	{
		return InternalMock.Object.ReadText( path );
	}

	public DateTime GetLastWriteTime( string path )
	{
		return InternalMock.Object.GetLastWriteTime( path );
	}
}