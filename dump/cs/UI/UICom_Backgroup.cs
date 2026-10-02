using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_Backgroup : GComponent
{
	public Controller showLine;

	public GGraph loader_BG;

	public GLoader loader_Line;

	public GLoader loader_SystemBG;

	public const string URL = "ui://xuaw6o8jbooc3v";

	public static UICom_Backgroup CreateInstance()
	{
		return (UICom_Backgroup)UIPackage.CreateObject("Common", "Com_Backgroup");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		showLine = GetControllerAt(0);
		loader_BG = (GGraph)GetChildAt(1);
		loader_Line = (GLoader)GetChildAt(2);
		loader_SystemBG = (GLoader)GetChildAt(3);
	}
}
