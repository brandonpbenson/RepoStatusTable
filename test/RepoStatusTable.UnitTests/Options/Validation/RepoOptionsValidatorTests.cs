using System.Collections.Generic;
using RepoStatusTable.Options;

namespace RepoStatusTable.UnitTests.Options.Validation;

public class RepoOptionsValidatorTests
{
	#region RepoDirs

	[Test]
	public void Validate_WithOnlyValidRepoDirs_ShouldReturnValidationSuccess()
	{
		var uut = new RepoOptionsValidatorBuilder()
			.WithIsVcsRepoReturns( true ).Build();

		var options = new RepoOptions
		{
			RepoDirs = new List<string> { "/a/path" }
		};

		var result = uut.Validate( "Lorem Ipsum", options );

		Assert.False( result.Failed );
		Assert.True( result.Succeeded );
	}

	[Test]
	public void Validate_WithOnlyInvalidRepoDirs_ShouldReturnValidationFailure()
	{
		var uut = new RepoOptionsValidatorBuilder()
			.WithIsVcsRepoReturns( false ).Build();

		var options = new RepoOptions
		{
			RepoDirs = new List<string> { "/a/path" }
		};

		var result = uut.Validate( "Lorem Ipsum", options );

		Assert.True( result.Failed );
		Assert.False( result.Succeeded );
	}

	[Test]
	public void Validate_WithValidAndInvalidRepoDirs_ShouldReturnValidationFailure()
	{
		const string validPath = "/valid/path";
		const string invalidPath = "/invalid/path";

		var uut = new RepoOptionsValidatorBuilder()
			.WithIsVcsRepoReturns( true, validPath )
			.WithIsVcsRepoReturns( false, invalidPath )
			.Build();

		var options = new RepoOptions
		{
			RepoDirs = new List<string> { validPath, invalidPath }
		};

		var result = uut.Validate( "Lorem Ipsum", options );

		Assert.True( result.Failed );
		Assert.False( result.Succeeded );
	}

	#endregion

	#region ReposRoot

	[Test]
	public void Validate_WithOnlyValidReposRoots_ShouldReturnValidationSuccess()
	{
		var uut = new RepoOptionsValidatorBuilder()
			.WithDirectoryExists( true ).Build();

		var options = new RepoOptions
		{
			RepoRoots = new List<string> { "/a/path" }
		};

		var result = uut.Validate( "Lorem Ipsum", options );

		Assert.False( result.Failed );
		Assert.True( result.Succeeded );
	}

	[Test]
	public void Validate_WithOnlyInvalidRepoRoots_ShouldReturnValidationFailure()
	{
		var uut = new RepoOptionsValidatorBuilder()
			.WithDirectoryExists( false ).Build();

		var options = new RepoOptions
		{
			RepoRoots = new List<string> { "/a/path" }
		};

		var result = uut.Validate( "Lorem Ipsum", options );

		Assert.True( result.Failed );
		Assert.False( result.Succeeded );
	}

	[Test]
	public void Validate_WithValidAndInvalidRepoRoots_ShouldReturnValidationFailure()
	{
		const string validPath = "/valid/path";
		const string invalidPath = "/invalid/path";

		var uut = new RepoOptionsValidatorBuilder()
			.WithDirectoryExists( true, validPath )
			.WithDirectoryExists( false, invalidPath )
			.Build();

		var options = new RepoOptions
		{
			RepoRoots = new List<string> { validPath, invalidPath }
		};

		var result = uut.Validate( "Lorem Ipsum", options );

		Assert.True( result.Failed );
		Assert.False( result.Succeeded );
	}

	#endregion
}