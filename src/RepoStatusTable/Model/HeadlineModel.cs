using RepoStatusTable.Options;

namespace RepoStatusTable.Model;

public class HeadlineModel : IHeadlineModel
{
	private readonly HeadlineOptions _options;

	public HeadlineModel( IOptions<HeadlineOptions> options )
	{
		_options = options.Value;
	}

	public string GetHeadline()
	{
		return _options.Text;
	}
}