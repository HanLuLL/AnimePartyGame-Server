using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGachaInfo_Com_Record : GComponent
{
	public GTextField txt_PoolName;

	public GTextField txt_Explain;

	public GList list_Record;

	public GButton btn_Left;

	public GTextField txt_Page;

	public GButton btn_Right;

	public const string URL = "ui://egrtucyhot0w6";

	public static UIGachaInfo_Com_Record CreateInstance()
	{
		return (UIGachaInfo_Com_Record)UIPackage.CreateObject("GachaInfo", "GachaInfo_Com_Record");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_PoolName = (GTextField)GetChildAt(0);
		txt_Explain = (GTextField)GetChildAt(2);
		list_Record = (GList)GetChildAt(12);
		btn_Left = (GButton)GetChildAt(14);
		txt_Page = (GTextField)GetChildAt(15);
		btn_Right = (GButton)GetChildAt(16);
	}
}
