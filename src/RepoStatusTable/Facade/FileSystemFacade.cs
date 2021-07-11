using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RepoStatusTable.Facade
{
	/// <summary>
	/// Facade for System.IO
	/// Handles all calls that directly access information about the file system
	/// </summary>
	public interface IFileSystemFacade
	{
		string GetFullPath( string path );

		IEnumerable<string> GetDirectories( string path );

		string GetDirectoryName( string path );

		string Join( string path1, string path2 );

		bool DirectoryExists( string path );

		Task<string> ReadAllText( string path );
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

		public string Join( string path1, string path2 )
		{
			return Path.Join( path1, path2 );
		}

		public bool DirectoryExists( string path )
		{
			return Directory.Exists( path );
		}

		public Task<string> ReadAllText( string path )
		{
			return File.ReadAllTextAsync( path );
		}

		public bool FileExists( string path )
		{
			return File.Exists( path );
		}
	}
}