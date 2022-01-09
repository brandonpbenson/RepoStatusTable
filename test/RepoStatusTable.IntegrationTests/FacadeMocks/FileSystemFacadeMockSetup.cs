using System;
using Microsoft.Extensions.DependencyInjection;
using RepoStatusTable.Facade;

namespace RepoStatusTable.IntegrationTests.FacadeMocks;

public class FileSystemFacadeMockSetup
{
	private readonly FileSystemFacadeMock _instance;

	public FileSystemFacadeMockSetup( IServiceProvider provider )
	{
		_instance = provider.GetRequiredService<IFileSystemFacade>() as FileSystemFacadeMock ??
		            throw new InvalidOperationException();
	}

	public FileSystemFacadeMockSetup DirectoryExistsReturnsForPath( string path, bool returnValue )
	{
		_instance.InternalMock.Setup( m => m.DirectoryExists( path ) ).Returns( returnValue );
		return this;
	}

	public FileSystemFacadeMockSetup GetFullPathReturnsForPath( string path, string fullPath )
	{
		_instance.InternalMock.Setup( m => m.GetFullPath( path ) ).Returns( fullPath );
		return this;
	}

	public FileSystemFacadeMockSetup GetDirectoryName( string path, string dirname )
	{
		_instance.InternalMock.Setup( m => m.GetDirectoryName( path ) ).Returns( dirname );
		return this;
	}
}