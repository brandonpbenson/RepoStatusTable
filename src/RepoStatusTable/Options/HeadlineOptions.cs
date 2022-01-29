using RepoStatusTable.Options.Validation;

namespace RepoStatusTable.Options;

public class HeadlineOptions
{
	public bool Enable { get; set; }

	[RequiredIfEnabled] public string Text { get; set; }
}