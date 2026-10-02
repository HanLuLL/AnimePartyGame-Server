using FairyGUI;
using FairyGUI.Utils;
using GameLogic;
using Tools;

namespace UI;

public class UIRoomHero_Button_SelectSkin : GButton
{
	public SkinStandingPaintingConfigureItem StandingPainting;

	public bool IsHas;

	public UIRoomHero_Com_SkinItem com_Loader;

	public GTextField txt_chrname;

	public Transition Cut_in;

	public const string URL = "ui://l82hrmsqec7w1v";

	public int SkinItemId
	{
		get
		{
			if (StandingPainting != null)
			{
				return StandingPainting.ItemID;
			}
			return 0;
		}
	}

	public void RefreshSkinItem(SkinStandingPaintingConfigureItem standingPainting)
	{
		StandingPainting = standingPainting;
		IsHas = standingPainting.IsDefault || SimpleSingletonProvider<GameLogicManager>.inst.bag.ExistItem(SkinItemId);
		com_Loader.loader_Character.url = standingPainting.GetCharacterPhoto();
		txt_chrname.text = standingPainting.ItemID.GetLocal(UIStringType.Item);
		base.touchable = true;
		base.grayed = !IsHas;
	}

	public static UIRoomHero_Button_SelectSkin CreateInstance()
	{
		return (UIRoomHero_Button_SelectSkin)UIPackage.CreateObject("RoomHero", "RoomHero_Button_SelectSkin");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_Loader = (UIRoomHero_Com_SkinItem)GetChildAt(3);
		txt_chrname = (GTextField)GetChildAt(4);
		Cut_in = GetTransitionAt(0);
	}
}
