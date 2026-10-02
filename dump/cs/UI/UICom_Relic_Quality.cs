using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_Relic_Quality : GComponent
{
	public Controller quality;

	public const string URL = "ui://xuaw6o8j9wy8bw";

	public static UICom_Relic_Quality CreateInstance()
	{
		return (UICom_Relic_Quality)UIPackage.CreateObject("Common", "Com_Relic_Quality");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		quality = GetControllerAt(0);
	}
}
