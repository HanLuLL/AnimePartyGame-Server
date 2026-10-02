using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISetting_Comp_Score_Desc : GComponent
{
	public GTextField txt_1;

	public GTextField txt_2;

	public GTextField txt_3;

	public GTextField txt_4;

	public GTextField txt_5;

	public GTextField txt_6;

	public const string URL = "ui://iy1joavthm5n27";

	public static UISetting_Comp_Score_Desc CreateInstance()
	{
		return (UISetting_Comp_Score_Desc)UIPackage.CreateObject("Setting", "Setting_Comp_Score_Desc");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_1 = (GTextField)GetChildAt(6);
		txt_2 = (GTextField)GetChildAt(7);
		txt_3 = (GTextField)GetChildAt(8);
		txt_4 = (GTextField)GetChildAt(9);
		txt_5 = (GTextField)GetChildAt(10);
		txt_6 = (GTextField)GetChildAt(11);
	}
}
