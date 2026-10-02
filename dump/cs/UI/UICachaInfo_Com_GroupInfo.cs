using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICachaInfo_Com_GroupInfo : GComponent
{
	public GTextField txt_groupDesc;

	public GList list_Item;

	public const string URL = "ui://egrtucyhot0w2";

	public static UICachaInfo_Com_GroupInfo CreateInstance()
	{
		return (UICachaInfo_Com_GroupInfo)UIPackage.CreateObject("GachaInfo", "CachaInfo_Com_GroupInfo");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_groupDesc = (GTextField)GetChildAt(1);
		list_Item = (GList)GetChildAt(2);
	}
}
