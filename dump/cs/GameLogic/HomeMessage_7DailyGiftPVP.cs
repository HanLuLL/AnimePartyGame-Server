using Cysharp.Threading.Tasks;
using Tools;
using UI;

namespace GameLogic;

public class HomeMessage_7DailyGiftPVP : HomeMessage
{
	public override void ShowMessage()
	{
		if (SimpleSingletonProvider<GameLogicManager>.inst.store.Get7DailyGiftStatus(ShopTabType.Day7GiftPackage))
		{
			SimpleSingletonProvider<UIManager>.inst.OpenPanel(UIPanelType.Store, ShopTabType.Day7GiftPackage).Forget();
		}
		else
		{
			NextMessage();
		}
	}
}
