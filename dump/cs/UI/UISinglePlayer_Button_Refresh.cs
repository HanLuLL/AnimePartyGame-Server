using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayer_Button_Refresh : GButton
{
	public Controller free;

	public GTextField txt_Gold;

	public const string URL = "ui://mi9vm3w0wcumq61";

	public static UISinglePlayer_Button_Refresh CreateInstance()
	{
		return (UISinglePlayer_Button_Refresh)UIPackage.CreateObject("SinglePlayer", "SinglePlayer_Button_Refresh");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		free = GetControllerAt(1);
		txt_Gold = (GTextField)GetChildAt(2);
	}
}
