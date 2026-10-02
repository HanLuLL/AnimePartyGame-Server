using GameLogic;
using Tools;

public abstract class HomeMessage
{
	public abstract void ShowMessage();

	protected void NextMessage()
	{
		SimpleSingletonProvider<GameLogicManager>.inst.home.TriggerMessage();
	}
}
