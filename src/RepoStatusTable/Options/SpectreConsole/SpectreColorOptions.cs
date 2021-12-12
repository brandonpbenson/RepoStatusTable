using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using RepoStatusTable.Utilities;
using Spectre.Console;

namespace RepoStatusTable.Options.SpectreConsole;

[SuppressMessage( "ReSharper", "UnusedAutoPropertyAccessor.Global" )]
[SuppressMessage( "ReSharper", "AutoPropertyCanBeMadeGetOnly.Global" )]
public class SpectreColorOptions
{
	public ColorNotation Notation { get; set; } = ColorNotation.Rgb;

	public string Value { get; set; } = "0,0,0";
}

public enum ColorNotation
{
	Name,
	Rgb,
	Hexadecimal
}

public static class SpectreColorExtensions
{
	public static Color GetSpectreConsoleColor( this SpectreColorOptions options )
	{
		return options.Notation switch
		{
			ColorNotation.Name => ColorFromName( options ),
			ColorNotation.Rgb => ColorFromRgb( options.Value ),
			ColorNotation.Hexadecimal => ColorFromHex( options.Value ),
			_ => throw new ArgumentOutOfRangeException( $"Invalid color notation given: {options.Notation} " )
		};
	}

	private static Color ColorFromName( SpectreColorOptions options )
	{
		return ReflectionUtility.GetValueOfProperty<Color, Color>( options.Value, new Color() );
	}

	private static Color ColorFromRgb( string rgbColorCode )
	{
		var splitColors = rgbColorCode.Split( "," );

		if ( splitColors.Length != 3 )
		{
			throw new ArgumentException( $"Invalid RGB color code given: {rgbColorCode}" );
		}

		var r = byte.Parse( splitColors[0] );
		var g = byte.Parse( splitColors[1] );
		var b = byte.Parse( splitColors[2] );

		return new Color( r, g, b );
	}

	private static Color ColorFromHex( string hexColorCode )
	{
		if ( hexColorCode.StartsWith( "#" ) )
		{
			hexColorCode = hexColorCode.Substring( 1, hexColorCode.Length - 1 );
		}

		if ( hexColorCode.Length != 6 )
		{
			throw new ArgumentException( $"Invalid Hex color code given: {hexColorCode}" );
		}

		(byte, byte, byte) GetRgbFromHex()
		{
			byte DecFromHex( int index )
			{
				return byte.Parse( hexColorCode.Substring( index, 2 ), NumberStyles.AllowHexSpecifier );
			}

			return ( DecFromHex( 0 ), DecFromHex( 2 ), DecFromHex( 4 ) );
		}

		var (r, g, b) = GetRgbFromHex();
		return new Color( r, g, b );
	}
}