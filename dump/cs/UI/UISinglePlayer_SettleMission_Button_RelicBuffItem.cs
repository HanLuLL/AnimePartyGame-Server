using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayer_SettleMission_Button_RelicBuffItem : GButton
{
	public Controller state;

	public GLoader loader_Icon;

	public GTextField txt_ChargeCount;

	public const string URL = "ui://mi9vm3w0tvj1q81";

	public static UISinglePlayer_SettleMission_Button_RelicBuffItem CreateInstance()
	{
		return (UISinglePlayer_SettleMission_Button_RelicBuffItem)UIPackage.CreateObject("SinglePlayer", "SinglePlayer_SettleMission_Button_RelicBuffItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		state = GetControllerAt(1);
		loader_Icon = (GLoader)GetChildAt(1);
		txt_ChargeCount = (GTextField)GetChildAt(2);
	}
}
