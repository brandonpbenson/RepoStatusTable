using RepoStatusTable.Options;

namespace RepoStatusTable.View;

public class HeadlineViewProxy : IHeadlineView
{
	private readonly HeadlineOptions _options;
	private readonly IHeadlineViewStrategy _strategy;

	public HeadlineViewProxy( IOptions<HeadlineOptions> options, IHeadlineViewStrategy strategy )
	{
		_options = options.Value;
		_strategy = strategy;
	}

	public Task RenderAsync()
	{
		return _options.Enable ? _strategy.RenderAsync() : Task.CompletedTask;
	}
}