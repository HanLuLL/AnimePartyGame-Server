using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISetting_Item_BottomBg : GComponent
{
	public GImage zheZhao;

	public const string URL = "ui://iy1joavto1n8a";

	public static UISetting_Item_BottomBg CreateInstance()
	{
		return (UISetting_Item_BottomBg)UIPackage.CreateObject("Setting", "Setting_Item_BottomBg");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		zheZhao = (GImage)GetChildAt(0);
	}
}
