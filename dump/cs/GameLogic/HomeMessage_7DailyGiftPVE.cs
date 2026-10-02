using Cysharp.Threading.Tasks;
using Tools;
using UI;

namespace GameLogic;

public class HomeMessage_7DailyGiftPVE : HomeMessage
{
	public override void ShowMessage()
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.store.Get7DailyGiftStatus(ShopTabType.Day7GiftPackagePve))
		{
			SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Store, ShopTabType.Day7GiftPackagePve).Forget();
		}
		else
		{
			NextMessage();
		}
	}
}
