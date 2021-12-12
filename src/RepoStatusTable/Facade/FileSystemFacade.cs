using System.Collections.Generic;
using System.IO;

namespace RepoStatusTable.Facade;

/// <summary>
///     Facade for <see cref="System.IO" />
///     Handles all calls that directly access information about the file system
/// </summary>
public interface IFileSystemFacade
{
	/// <inheritdoc cref="Path.GetFullPath(string)" />
	string GetFullPath( string path );

	/// <inheritdoc cref="Directory.GetDirectories(string)" />
	IEnumerable<string> GetDirectories( string path );

	/// <inheritdoc cref="DirectoryInfo.Name" />
	string GetDirectoryName( string path );

	/// <inheritdoc cref="Path.Join(string, string)" />
	string Join( string? path1, string? path2 );

	/// <inheritdoc cref="DirectoryExists" />
	bool DirectoryExists( string path );

	/// <inheritdoc cref="File.ReadLines(string)" />
	IEnumerable<string> ReadText( string path );
}

public class FileSystemFacade : IFileSystemFacade
{
	public string GetFullPath( string path )
	{
		return Path.GetFullPath( path );
	}

	public IEnumerable<string> GetDirectories( string path )
	{
		var fullPath = GetFullPath( path );
		return Directory.GetDirectories( fullPath );
	}

	public string GetDirectoryName( string path )
	{
		return new DirectoryInfo( path ).Name;
	}

	public string Join( string? path1, string? path2 )
	{
		return Path.Join( path1, path2 );
	}

	public bool DirectoryExists( string path )
	{
		return Directory.Exists( path );
	}

	public IEnumerable<string> ReadText( string path )
	{
		return File.ReadLines( path );
	}
}