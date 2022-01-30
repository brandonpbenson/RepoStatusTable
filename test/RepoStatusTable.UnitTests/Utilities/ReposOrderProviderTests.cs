using System;
using System.Collections.Generic;

namespace RepoStatusTable.UnitTests.Utilities;

public class ReposOrderProviderTests
{
	[Test]
	public void OrderAccordingToOptions_WithAlphabeticalOrder_ShouldReturnOrdered()
	{
		var uut = new ReposOrderProviderBuilder()
			.WithAscendingOrder()
			.WithOrderByAlphabetically()
			.Build();

		var unorderedDirectories = new List<string>
		{
			"c", "a", "b", "d"
		};

		var orderedDirectories = new List<string>
		{
			"a", "b", "c", "d"
		};

		var result = uut.OrderAccordingToOptions( unorderedDirectories );
		Assert.AreEqual( orderedDirectories, result );
	}

	[Test]
	public void OrderAccordingToOptions_WithDescendingAlphabeticalOrder_ShouldReturnOrdered()
	{
		var uut = new ReposOrderProviderBuilder()
			.WithDescendingOrder()
			.WithOrderByAlphabetically()
			.Build();

		var unorderedDirectories = new List<string>
		{
			"c", "a", "b", "d"
		};

		var orderedDirectories = new List<string>
		{
			"d", "c", "b", "a"
		};

		var result = uut.OrderAccordingToOptions( unorderedDirectories );
		Assert.AreEqual( orderedDirectories, result );
	}

	[Test]
	public void OrderAccordingToOptions_WithAscendingLastModifiedOrder_ShouldReturnOrdered()
	{
		var uut = new ReposOrderProviderBuilder()
			.WithAscendingOrder()
			.WithOrderByLastModified()
			.WithFileSystemFacadeLastWriteTimeReturns( "b", new DateTime( 2010, 1, 1 ) )
			.WithFileSystemFacadeLastWriteTimeReturns( "a", new DateTime( 2011, 1, 1 ) )
			.WithFileSystemFacadeLastWriteTimeReturns( "d", new DateTime( 2012, 1, 1 ) )
			.WithFileSystemFacadeLastWriteTimeReturns( "c", new DateTime( 2013, 1, 1 ) )
			.Build();

		var unorderedDirectories = new List<string>
		{
			"c", "a", "b", "d"
		};

		var orderedDirectories = new List<string>
		{
			"b", "a", "d", "c"
		};

		var result = uut.OrderAccordingToOptions( unorderedDirectories );
		Assert.AreEqual( orderedDirectories, result );
	}

	[Test]
	public void OrderAccordingToOptions_WithDescendingLastModifiedOrder_ShouldReturnOrdered()
	{
		var uut = new ReposOrderProviderBuilder()
			.WithDescendingOrder()
			.WithOrderByLastModified()
			.WithFileSystemFacadeLastWriteTimeReturns( "b", new DateTime( 2010, 1, 1 ) )
			.WithFileSystemFacadeLastWriteTimeReturns( "a", new DateTime( 2011, 1, 1 ) )
			.WithFileSystemFacadeLastWriteTimeReturns( "d", new DateTime( 2012, 1, 1 ) )
			.WithFileSystemFacadeLastWriteTimeReturns( "c", new DateTime( 2013, 1, 1 ) )
			.Build();

		var unorderedDirectories = new List<string>
		{
			"c", "a", "b", "d"
		};

		var orderedDirectories = new List<string>
		{
			"c", "d", "a", "b"
		};

		var result = uut.OrderAccordingToOptions( unorderedDirectories );
		Assert.AreEqual( orderedDirectories, result );
	}
}