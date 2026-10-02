using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UINGOStore_Button_GoStore : GButton
{
	public Controller Type;

	public GTextField Txt_Time;

	public const string URL = "ui://na6sy4s6kqgjj";

	public static UINGOStore_Button_GoStore CreateInstance()
	{
		return (UINGOStore_Button_GoStore)UIPackage.CreateObject("NGOStore", "NGOStore_Button_GoStore");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Type = GetControllerAt(1);
		Txt_Time = (GTextField)GetChildAt(3);
	}
}
