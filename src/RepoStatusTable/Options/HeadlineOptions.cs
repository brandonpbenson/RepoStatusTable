namespace RepoStatusTable.Options;

public class HeadlineOptions
{
	public bool Enable { get; set; } = false;

	[Required] public string Text { get; set; }
}