using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISetting_Com_Detail : GComponent
{
	public UISetting_Comp_Score_Desc com_desc;

	public GButton btn_quit;

	public GButton btn_ok;

	public const string URL = "ui://iy1joavto18025";

	public static UISetting_Com_Detail CreateInstance()
	{
		return (UISetting_Com_Detail)UIPackage.CreateObject("Setting", "Setting_Com_Detail");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_desc = (UISetting_Comp_Score_Desc)GetChildAt(1);
		btn_quit = (GButton)GetChildAt(2);
		btn_ok = (GButton)GetChildAt(3);
	}
}
