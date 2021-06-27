using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RepoStatusTable.Facade
{
	public interface IFileSystemFacade
	{
		string GetFullPath( string path );

		IEnumerable<string> GetDirectories( string path );

		string GetDirectoryName( string path );

		string Join( string path1, string path2 );

		bool Exists( string path );

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

		public bool Exists( string path )
		{
			return File.Exists( path );
		}

		public Task<string> ReadAllText( string path )
		{
			return File.ReadAllTextAsync( path );
		}
	}
}