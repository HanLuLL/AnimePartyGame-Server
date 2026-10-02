using GameLogic.Replay;

namespace UI;

public class ReplayTurnItemVM : ReplayNodeListItemVM
{
	public ReplayTurnNode Turn;

	public CharacterType roleType;

	public string URL;

	public bool IsCurrent;

	public ReplayTurnItemVM(ReplayTurnNode turn)
		: base(ReplayNodeItemType.Turn)
	{
		Turn = turn;
	}
}
