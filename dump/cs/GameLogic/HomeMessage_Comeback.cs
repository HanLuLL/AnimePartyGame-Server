using Core;
using Tools;
using UI;

namespace GameLogic;

public class HomeMessage_Comeback : HomeMessage
{
	public override async void ShowMessage()
	{
		ComebackData comebackData = SimpleSingletonProvider<GameLogicManager>.inst.comeback?.Data;
		if (comebackData != null && comebackData.IsInPeriod() && LocalCache.GetComebackEndTime() != comebackData.ReturnInfo.EndTime)
		{
			LocalCache.UpdateComebackTips(comebackData.ReturnInfo.EndTime);
			await SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.ActivityComeback);
		}
	}
}
