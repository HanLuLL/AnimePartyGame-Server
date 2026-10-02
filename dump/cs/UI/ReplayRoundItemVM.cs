using GameLogic.Replay;

namespace UI;

public class ReplayRoundItemVM : ReplayNodeListItemVM
{
	public ReplayRoundNode Round;

	public bool IsExpanded;

	public string Title;

	public ReplayRoundItemVM(ReplayRoundNode round)
		: base(ReplayNodeItemType.Round)
	{
		Round = round;
	}
}
