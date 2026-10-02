using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStoreSeason_Com_ItemMask : GComponent
{
	public GImage zhezhao;

	public const string URL = "ui://begz6gfv7vwm11";

	public static UIActivityStoreSeason_Com_ItemMask CreateInstance()
	{
		return (UIActivityStoreSeason_Com_ItemMask)UIPackage.CreateObject("ActivityStoreSeason", "ActivityStoreSeason_Com_ItemMask");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		zhezhao = (GImage)GetChildAt(0);
	}
}
