using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_PlayerLabel_Loader : GComponent
{
	public UICom_Loader_PlayerLabel com_Loader;

	public const string URL = "ui://xuaw6o8jaepoa6";

	public static UICom_PlayerLabel_Loader CreateInstance()
	{
		return (UICom_PlayerLabel_Loader)UIPackage.CreateObject("Common", "Com_PlayerLabel_Loader");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_Loader = (UICom_Loader_PlayerLabel)GetChildAt(0);
	}
}
