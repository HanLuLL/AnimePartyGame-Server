using Tools;

namespace GameLogic;

public class FightSignal
{
	public readonly Signal<bool, UIPanelType> readyFight = new Signal<bool, UIPanelType>();

	public readonly Signal<int> dodgeShow = new Signal<int>();
}
