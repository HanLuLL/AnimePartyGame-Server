namespace UI;

public abstract class ReplayNodeListItemVM
{
	public ReplayNodeItemType Type;

	protected ReplayNodeListItemVM(ReplayNodeItemType type)
	{
		Type = type;
	}
}
