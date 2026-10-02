using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISetting_Button_UID : GButton
{
	public GTextField txt_Uid;

	public GTextField uid;

	public const string URL = "ui://iy1joavtp5xi1s";

	public static UISetting_Button_UID CreateInstance()
	{
		return (UISetting_Button_UID)UIPackage.CreateObject("Setting", "Setting_Button_UID");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Uid = (GTextField)GetChildAt(1);
		uid = (GTextField)GetChildAt(2);
	}
}
