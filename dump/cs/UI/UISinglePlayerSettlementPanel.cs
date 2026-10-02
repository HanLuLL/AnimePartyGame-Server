using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayerSettlementPanel : GComponent
{
	public Controller status;

	public Controller language;

	public UISettlement_com_end SingPlayerSettlement_com_end;

	public GButton Exit_Btn;

	public Transition Lose_Cut_in;

	public Transition Win_Cut_in;

	public const string URL = "ui://wwhrkd30sdg20";

	public static UISinglePlayerSettlementPanel CreateInstance()
	{
		BindAll();
		return (UISinglePlayerSettlementPanel)UIPackage.CreateObject("SinglePlayerSettlement", "SinglePlayerSettlementPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://wwhrkd30hh8o11", typeof(UISettlement_Btn_Coin));
		UIObjectFactory.SetPackageItemExtension("ui://wwhrkd30hh8oy", typeof(UISettlement_Com_Info));
		UIObjectFactory.SetPackageItemExtension("ui://wwhrkd30hh8oz", typeof(UISettlement_Btn_Score));
		UIObjectFactory.SetPackageItemExtension("ui://wwhrkd30sdg20", typeof(UISinglePlayerSettlementPanel));
		UIObjectFactory.SetPackageItemExtension("ui://wwhrkd30uytml", typeof(UISettlement_com_end));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(0);
		language = GetControllerAt(1);
		SingPlayerSettlement_com_end = (UISettlement_com_end)GetChildAt(4);
		Exit_Btn = (GButton)GetChildAt(13);
		Lose_Cut_in = GetTransitionAt(0);
		Win_Cut_in = GetTransitionAt(1);
	}
}
