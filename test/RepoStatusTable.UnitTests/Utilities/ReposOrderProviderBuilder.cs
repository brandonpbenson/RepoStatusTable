using System;
using Microsoft.Extensions.Options;
using RepoStatusTable.Facade;
using RepoStatusTable.Options;
using RepoStatusTable.Utilities.ReposDirectory;

namespace RepoStatusTable.UnitTests.Utilities;

public class ReposOrderProviderBuilder
{
	private readonly Mock<IFileSystemFacade> _fileSystemFacade = new(MockBehavior.Strict);
	private readonly RepoOptions _options = new();

	public ReposOrderProviderBuilder WithDescendingOrder()
	{
		_options.Order = RepoOrder.Descending;
		return this;
	}

	public ReposOrderProviderBuilder WithAscendingOrder()
	{
		_options.Order = RepoOrder.Ascending;
		return this;
	}

	public ReposOrderProviderBuilder WithOrderByAlphabetically()
	{
		_options.OrderBy = RepoOrderBy.Alphabetically;
		return this;
	}

	public ReposOrderProviderBuilder WithOrderByLastModified()
	{
		_options.OrderBy = RepoOrderBy.LastModified;
		return this;
	}

	public ReposOrderProviderBuilder WithFileSystemFacadeLastWriteTimeReturns( string path, DateTime dateTime )
	{
		_fileSystemFacade.Setup( m => m.GetLastWriteTime( path ) )
			.Returns( dateTime );
		return this;
	}

	public ReposOrderProvider Build()
	{
		return new ReposOrderProvider(
			new OptionsWrapper<RepoOptions>( _options ),
			_fileSystemFacade.Object );
	}
}