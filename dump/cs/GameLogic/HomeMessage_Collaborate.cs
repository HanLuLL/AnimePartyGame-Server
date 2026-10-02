using Tools;
using UI;

namespace GameLogic;

public class HomeMessage_Collaborate : HomeMessage
{
	public override async void ShowMessage()
	{
		CollaborationInfoConfigure collaborationInfoConfigure = SimpleSingletonProvider<GameLogicManager>.inst.collaborate.TryGetCollaboration();
		if (collaborationInfoConfigure != null)
		{
			foreach (int item in collaborationInfoConfigure.HeroID)
			{
				if (!SimpleSingletonProvider<GameLogicManager>.inst.heroCard.GetCardData(item).IsHas)
				{
					if (collaborationInfoConfigure.UIWindowType == UIWindowType.WitchWeapon)
					{
						await SimpleSingletonProvider<UIManager>.inst.witchWeapon.ShowWitchWeaponSkin(collaborationInfoConfigure.Id);
					}
					else if (collaborationInfoConfigure.UIWindowType == UIWindowType.Va11HallA)
					{
						await SimpleSingletonProvider<UIManager>.inst.VA11HallA.ShowVA11HallASkin(collaborationInfoConfigure, base.NextMessage);
					}
					else if (collaborationInfoConfigure.UIWindowType == UIWindowType.Ngostore)
					{
						await SimpleSingletonProvider<UIManager>.inst.ngoStore.ShowNGOHero(collaborationInfoConfigure, base.NextMessage);
					}
					else if (collaborationInfoConfigure.UIWindowType == UIWindowType.Mgwtstore)
					{
						await SimpleSingletonProvider<UIManager>.inst.mgwtStoreWindow.ShowMGWTStore(collaborationInfoConfigure, base.NextMessage);
					}
					return;
				}
			}
		}
		NextMessage();
	}
}
