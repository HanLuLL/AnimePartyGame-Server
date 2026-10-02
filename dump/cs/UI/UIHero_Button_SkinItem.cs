using Cysharp.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;

namespace UI;

public class UIHero_Button_SkinItem : GButton
{
	public bool IsHas;

	public Controller Using;

	public UIHero_Com_SkinImage com_SkinImage;

	public Transition Cut_in;

	public const string URL = "ui://7qkd4lqxq93cq2t";

	public void Refresh(SkinStandingPaintingConfigureItem skinStandingPaintingConfigureItem)
	{
		IsHas = skinStandingPaintingConfigureItem.IsDefault || SimpleSingletonProvider<GameLogicManager>.inst.bag.ExistItem(skinStandingPaintingConfigureItem.ItemID);
		com_SkinImage.loader_Skin.url = skinStandingPaintingConfigureItem.GetCharacterReady();
		base.title = skinStandingPaintingConfigureItem.ItemID.GetItemInfoConfigure().NameID.GetLocal(UIStringType.Item);
		((UICom_SkinQuality)com_SkinImage.com_Qulity).appearanceType.selectedIndex = (int)skinStandingPaintingConfigureItem.SkinAppearanceType;
		if (IsHas && skinStandingPaintingConfigureItem.SkinAppearanceType == SkinAppearanceType.Ultimate)
		{
			EffectInfoConfigure effectDataConfigure = 55.GetEffectDataConfigure();
			if (effectDataConfigure != null)
			{
				com_SkinImage.graph_Effect.visible = true;
				SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(effectDataConfigure.EffectName, com_SkinImage.graph_Effect, 125f).Forget();
			}
		}
		else
		{
			com_SkinImage.graph_Effect.visible = false;
		}
		base.grayed = !IsHas;
	}

	public override void Dispose()
	{
		SimpleSingletonProvider<GameObjectManager>.inst.Stop(com_SkinImage.graph_Effect);
		base.Dispose();
	}

	public static UIHero_Button_SkinItem CreateInstance()
	{
		return (UIHero_Button_SkinItem)UIPackage.CreateObject("Hero", "Hero_Button_SkinItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Using = GetControllerAt(1);
		com_SkinImage = (UIHero_Com_SkinImage)GetChildAt(3);
		Cut_in = GetTransitionAt(0);
	}
}
