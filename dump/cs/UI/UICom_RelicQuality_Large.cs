using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_RelicQuality_Large : GComponent
{
	public Controller qualityType;

	public const string URL = "ui://xuaw6o8jfu2tq3e";

	public static UICom_RelicQuality_Large CreateInstance()
	{
		return (UICom_RelicQuality_Large)UIPackage.CreateObject("Common", "Com_RelicQuality_Large");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		qualityType = GetControllerAt(0);
	}
}
