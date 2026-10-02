using Tools;

namespace GameLogic;

public class AccountSignal
{
	public readonly Signal levelChanged = new Signal();

	public readonly Signal<bool> changeName = new Signal<bool>();

	public readonly Signal<bool> changeFriendNote = new Signal<bool>();
}
