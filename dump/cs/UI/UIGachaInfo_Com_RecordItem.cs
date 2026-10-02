using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGachaInfo_Com_RecordItem : GComponent
{
	public GTextField txt_type;

	public GTextField txt_Name;

	public GTextField txt_Num;

	public GTextField txt_Time;

	public const string URL = "ui://egrtucyhot0w7";

	public static UIGachaInfo_Com_RecordItem CreateInstance()
	{
		return (UIGachaInfo_Com_RecordItem)UIPackage.CreateObject("GachaInfo", "GachaInfo_Com_RecordItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_type = (GTextField)GetChildAt(0);
		txt_Name = (GTextField)GetChildAt(1);
		txt_Num = (GTextField)GetChildAt(2);
		txt_Time = (GTextField)GetChildAt(3);
	}
}
