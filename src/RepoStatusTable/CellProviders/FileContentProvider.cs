using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RepoStatusTable.Facade;
using RepoStatusTable.Options.CellProvider;

namespace RepoStatusTable.CellProviders
{
	public class FileContentProvider : ICellProvider
	{
		private readonly IFileSystemFacade _fileSystemFacade;
		private readonly FileContentProviderOptions _options;

		public FileContentProvider( IOptions<FileContentProviderOptions> options, IFileSystemFacade fileSystemFacade )
		{
			_options = options.Value;
			_fileSystemFacade = fileSystemFacade;
		}

		public string Heading => _options.Heading;

		public bool IsEnabled => _options.Enable;

		public int? Position => _options.Position;

		public Task<Cell> GetCell( string path )
		{
			var cell = new Cell( ReadFileContentAsync( path ) );
			return Task.FromResult( cell );
		}

		private string ReadFileContentAsync( string? path )
		{
			var absolutePath = _fileSystemFacade.Join( path, _options.Path );
			IEnumerable<string> lines;

			try
			{
				lines = _fileSystemFacade.ReadText( absolutePath )
					.Take( _options.NumberOfLines );
			}
			catch ( Exception e ) when ( e is UnauthorizedAccessException or FileNotFoundException )
			{
				if ( _options.IgnoreMissingFiles == false )
					throw new ArgumentException( $"Could not access file {_options.Path} in repo {path}", e );

				return "";
			}

			return string.Join( "\n", lines );
		}
	}
}