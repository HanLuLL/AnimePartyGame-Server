using Cysharp.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Google.Protobuf.Collections;
using Tools;

namespace UI;

public class UIHero_Button_Hero : GButton
{
	private const int SortDefault = 0;

	private const int SortFavor = 1;

	private const int SortPve = 2;

	public int HeroId;

	public Controller breakthrough;

	public Controller Collaborate;

	public UIHero_Com_HeroItem com_Loader;

	public GTextField txt_chrname;

	public GTextField txt_Trial;

	public GButton btn_Collect;

	public UIHero_com_show com_show;

	public Transition Cut_in;

	public const string URL = "ui://7qkd4lqxg1lkb";

	public void Refresh(HeroCardData heroCard, int sortIndex = 0)
	{
		HeroId = heroCard.HeroId;
		com_Loader.loader_Character.url = heroCard.standingPainting.GetCharacterPhoto();
		((UICom_SkinQuality)com_Loader.com_Qulity).appearanceType.selectedIndex = (int)heroCard.standingPainting.SkinAppearanceType;
		com_Loader.Ultimate.selectedIndex = ((heroCard.standingPainting.SkinAppearanceType == SkinAppearanceType.Ultimate) ? 1 : 0);
		if (heroCard.IsHas && heroCard.standingPainting.SkinAppearanceType == SkinAppearanceType.Ultimate)
		{
			EffectInfoConfigure effectDataConfigure = 55.GetEffectDataConfigure();
			if (effectDataConfigure != null)
			{
				com_Loader.graph_Effect.visible = true;
				SimpleSingletonProvider<GameObjectManager>.inst.ShowEffectInUI(effectDataConfigure.EffectName, com_Loader.graph_Effect, 78f).Forget();
			}
		}
		else
		{
			com_Loader.graph_Effect.visible = false;
		}
		txt_chrname.text = heroCard.InfoConfig.NickID.GetLocal(UIStringType.Character);
		bool isBreakThrough = heroCard.isBreakThrough;
		breakthrough.selectedIndex = (isBreakThrough ? 1 : 0);
		Collaborate.selectedIndex = (heroCard.InfoConfig.IsLinkage ? 1 : 0);
		txt_Trial.visible = !SimpleSingletonProvider<UIManager>.inst.guide.isShowing && heroCard.heroStatus != HeroStatus.Activate && heroCard.heroStatus != HeroStatus.None;
		base.grayed = heroCard.heroStatus == HeroStatus.None;
		RefreshSortShow(heroCard, sortIndex);
		UpdateCollectStatus(heroCard.CollectStatus);
	}

	private void RefreshSortShow(HeroCardData heroCard, int sortIndex)
	{
		if (com_show == null)
		{
			return;
		}
		switch (sortIndex)
		{
		case 1:
			com_show.Status.selectedIndex = 1;
			if (!heroCard.InfoConfig.HasKizuna)
			{
				RepeatedField<FavorLevelConfigure> levels = StaticConfigure.Favor.Levels;
				FavorLevelConfigure favorLevelConfigure = levels[levels.Count - 1];
				com_show.LV_txt.text = favorLevelConfigure.Id.ToString();
			}
			else
			{
				com_show.LV_txt.text = heroCard.LV.ToString();
			}
			break;
		case 2:
		{
			PVENurturanceBreakConfigure currentTalentConfigure = heroCard.PveData.GetCurrentTalentConfigure();
			com_show.Status.selectedIndex = ((currentTalentConfigure != null && heroCard.PveData.IsTalentUnlock(currentTalentConfigure.Id)) ? 3 : 2);
			com_show.LV_txt.text = heroCard.PveData.Level.ToString();
			break;
		}
		default:
			com_show.Status.selectedIndex = 0;
			com_show.LV_txt.text = "";
			break;
		}
	}

	public void UpdateCollectStatus(bool status)
	{
		btn_Collect.visible = status;
		if (btn_Collect is UIButton_Collect uIButton_Collect)
		{
			uIButton_Collect.collected.selectedIndex = 1;
		}
	}

	public override void Dispose()
	{
		SimpleSingletonProvider<GameObjectManager>.inst.Stop(com_Loader.graph_Effect);
		base.Dispose();
	}

	public static UIHero_Button_Hero CreateInstance()
	{
		return (UIHero_Button_Hero)UIPackage.CreateObject("Hero", "Hero_Button_Hero");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		breakthrough = GetControllerAt(1);
		Collaborate = GetControllerAt(2);
		com_Loader = (UIHero_Com_HeroItem)GetChildAt(4);
		txt_chrname = (GTextField)GetChildAt(5);
		txt_Trial = (GTextField)GetChildAt(6);
		btn_Collect = (GButton)GetChildAt(7);
		com_show = (UIHero_com_show)GetChildAt(8);
		Cut_in = GetTransitionAt(0);
	}
}
