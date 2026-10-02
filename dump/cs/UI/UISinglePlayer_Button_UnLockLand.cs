using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayer_Button_UnLockLand : GButton
{
	public GTextField txt_UnlockLand;

	public const string URL = "ui://mi9vm3w0wcumq62";

	public static UISinglePlayer_Button_UnLockLand CreateInstance()
	{
		return (UISinglePlayer_Button_UnLockLand)UIPackage.CreateObject("SinglePlayer", "SinglePlayer_Button_UnLockLand");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_UnlockLand = (GTextField)GetChildAt(3);
	}
}
