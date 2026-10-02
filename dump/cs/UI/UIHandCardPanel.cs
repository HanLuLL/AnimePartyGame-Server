using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHandCardPanel : GComponent
{
	public Controller launchHotZone;

	public UIHandCard_Com_CardHotZone hotZone;

	public GComponent container_Card;

	public GButton btn_Move;

	public UIHandCard_Com_Skill com_Skill;

	public GTextField txt_CardState;

	public const string URL = "ui://vflhnh8dorg71";

	public static UIHandCardPanel CreateInstance()
	{
		BindAll();
		return (UIHandCardPanel)UIPackage.CreateObject("HandCard", "HandCardPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://vflhnh8dorg71", typeof(UIHandCardPanel));
		UIObjectFactory.SetPackageItemExtension("ui://vflhnh8dorg7c", typeof(UIHandCard_Com_CardHotZone));
		UIObjectFactory.SetPackageItemExtension("ui://vflhnh8dorg7d", typeof(UIHandCard_Button_Card));
		UIObjectFactory.SetPackageItemExtension("ui://vflhnh8dqs184m", typeof(UIHandCard_Com_Skill));
		UIObjectFactory.SetPackageItemExtension("ui://vflhnh8dqs184n", typeof(UIHandCard_Button_Skill));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		launchHotZone = GetControllerAt(0);
		hotZone = (UIHandCard_Com_CardHotZone)GetChildAt(0);
		container_Card = (GComponent)GetChildAt(1);
		btn_Move = (GButton)GetChildAt(2);
		com_Skill = (UIHandCard_Com_Skill)GetChildAt(3);
		txt_CardState = (GTextField)GetChildAt(4);
	}
}
