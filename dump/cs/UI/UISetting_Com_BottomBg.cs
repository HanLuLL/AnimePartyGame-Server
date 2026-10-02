using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISetting_Com_BottomBg : GComponent
{
	public GImage zheZhao;

	public const string URL = "ui://iy1joavto1n85";

	public static UISetting_Com_BottomBg CreateInstance()
	{
		return (UISetting_Com_BottomBg)UIPackage.CreateObject("Setting", "Setting_Com_BottomBg");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		zheZhao = (GImage)GetChildAt(0);
	}
}
