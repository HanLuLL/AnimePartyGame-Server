using Tools;

namespace GameLogic;

public class HomeMessage_MonthCard : HomeMessage
{
	public override void ShowMessage()
	{
		if (!SimpleSingletonProvider<GameLogicManager>.inst.store.GetMonthlyCardConfig().TriggerMonthCard(base.NextMessage))
		{
			NextMessage();
		}
	}
}
