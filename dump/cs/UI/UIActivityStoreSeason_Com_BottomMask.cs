using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStoreSeason_Com_BottomMask : GComponent
{
	public GImage zheZhao;

	public const string URL = "ui://begz6gfv7vwmw";

	public static UIActivityStoreSeason_Com_BottomMask CreateInstance()
	{
		return (UIActivityStoreSeason_Com_BottomMask)UIPackage.CreateObject("ActivityStoreSeason", "ActivityStoreSeason_Com_BottomMask");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		zheZhao = (GImage)GetChildAt(0);
	}
}
