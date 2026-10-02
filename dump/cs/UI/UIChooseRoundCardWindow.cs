using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIChooseRoundCardWindow : GComponent
{
	public GGraph mohu;

	public GButton btn_ChooseCard;

	public GList list_card;

	public const string URL = "ui://335msnc1kxm52u";

	public static UIChooseRoundCardWindow CreateInstance()
	{
		BindAll();
		return (UIChooseRoundCardWindow)UIPackage.CreateObject("ChooseRoundCard", "ChooseRoundCardWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://335msnc1kxm50", typeof(UICard_Com_RoundCard));
		UIObjectFactory.SetPackageItemExtension("ui://335msnc1kxm52u", typeof(UIChooseRoundCardWindow));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		mohu = (GGraph)GetChildAt(0);
		btn_ChooseCard = (GButton)GetChildAt(1);
		list_card = (GList)GetChildAt(2);
	}
}
