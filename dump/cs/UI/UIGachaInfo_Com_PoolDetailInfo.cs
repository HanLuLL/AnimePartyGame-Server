using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGachaInfo_Com_PoolDetailInfo : GComponent
{
	public GTextField txt_PoolDesc;

	public GList list_groupInfo;

	public const string URL = "ui://egrtucyhot0w1";

	public static UIGachaInfo_Com_PoolDetailInfo CreateInstance()
	{
		return (UIGachaInfo_Com_PoolDetailInfo)UIPackage.CreateObject("GachaInfo", "GachaInfo_Com_PoolDetailInfo");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_PoolDesc = (GTextField)GetChildAt(0);
		list_groupInfo = (GList)GetChildAt(1);
	}
}
