using Moq;
using RepoStatusTable.Facade;
using RepoStatusTable.Options.Validation;

namespace RepoStatusTable.UnitTests.Options.Validation;

public class RepoOptionsValidatorBuilder
{
	private readonly Mock<IFileSystemFacade> _fileSystemFacade = new(MockBehavior.Strict);
	private readonly Mock<IVcsFacade> _vcsFacade = new(MockBehavior.Strict);

	public RepoOptionsValidator Build()
	{
		return new RepoOptionsValidator( _vcsFacade.Object,
			_fileSystemFacade.Object );
	}

	public RepoOptionsValidatorBuilder WithIsVcsRepoReturns( bool returnValue )
	{
		_vcsFacade.Setup( m => m.IsVcsRepo( It.IsAny<string>() ) )
			.Returns( returnValue );
		return this;
	}

	public RepoOptionsValidatorBuilder WithIsVcsRepoReturns( bool returnValue, string path )
	{
		_vcsFacade.Setup( m => m.IsVcsRepo( path ) )
			.Returns( returnValue );
		return this;
	}

	public RepoOptionsValidatorBuilder WithDirectoryExists( bool returnValue )
	{
		_fileSystemFacade.Setup( m => m.DirectoryExists( It.IsAny<string>() ) )
			.Returns( returnValue );
		return this;
	}

	public RepoOptionsValidatorBuilder WithDirectoryExists( bool returnValue, string path )
	{
		_fileSystemFacade.Setup( m => m.DirectoryExists( path ) )
			.Returns( returnValue );
		return this;
	}
}