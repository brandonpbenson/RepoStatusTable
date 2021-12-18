using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using RepoStatusTable.CellProviders;

namespace RepoStatusTable.UnitTests.CellProviders;

public class CellProviderManagerTests
{
	private class StubCellProvider : ICellProvider
	{
		public string Heading { get; init; }
		public bool IsEnabled { get; init; }
		public int? Position { get; init; }

		public Task<Cell> GetCell( string path )
		{
			throw new NotImplementedException();
		}
	}

	#region EnabledDisabledTests

	[Test]
	public void GetOrderedListOfEnabledCellProviders_WithNoEnabledCellProviders_ShouldReturnEmptyList()
	{
		var provider = new StubCellProvider
		{
			IsEnabled = false
		};

		var uut = new CellProviderManager( new[] { provider } );

		var result = uut.GetOrderedListOfEnabledCellProviders();

		Assert.IsEmpty( result );
	}

	[Test]
	public void GetOrderedListOfEnabledCellProviders_WithOneEnabledCellProviders_ShouldReturnOnlyEnabled()
	{
		var disabledProvider = new StubCellProvider
		{
			IsEnabled = false,
			Heading = "DisabledCellProvider"
		};

		var enabledProvider = new StubCellProvider
		{
			IsEnabled = true,
			Heading = "EnabledCellProvider"
		};

		var uut = new CellProviderManager( new[] { enabledProvider, disabledProvider } );

		var result = uut.GetOrderedListOfEnabledCellProviders();

		Assert.AreEqual( 1, result.Count );
		Assert.AreEqual( "EnabledCellProvider", result.First().Heading );
	}

	#endregion

	#region OrderTests

	[Test]
	public void GetOrderedListOfEnabledCellProviders_WithThreeOrderedCellProviders_ShouldReturnOrderedList()
	{
		var firstProvider = new StubCellProvider
		{
			IsEnabled = true,
			Position = 1
		};

		var secondProvider = new StubCellProvider
		{
			IsEnabled = true,
			Position = 2
		};

		var thirdProvider = new StubCellProvider
		{
			IsEnabled = true,
			Position = 3
		};

		var uut = new CellProviderManager( new[] { thirdProvider, firstProvider, secondProvider } );

		var result = uut.GetOrderedListOfEnabledCellProviders();

		Assert.AreEqual( 1, result[0].Position );
		Assert.AreEqual( 2, result[1].Position );
		Assert.AreEqual( 3, result[2].Position );
	}

	[Test]
	public void GetOrderedListOfEnabledCellProviders_WithOneUnorderedTwoOrderedCellProviders_ShouldReturnUnorderedLast()
	{
		var firstProvider = new StubCellProvider
		{
			IsEnabled = true,
			Position = 1
		};

		var secondProvider = new StubCellProvider
		{
			IsEnabled = true,
			Position = 2
		};

		var unorderedProvider = new StubCellProvider
		{
			IsEnabled = true,
			Position = null
		};

		var uut = new CellProviderManager( new[] { unorderedProvider, firstProvider, secondProvider } );

		var result = uut.GetOrderedListOfEnabledCellProviders();

		Assert.AreEqual( 1, result[0].Position );
		Assert.AreEqual( 2, result[1].Position );
		Assert.AreEqual( null, result[2].Position );
	}

	#endregion
}