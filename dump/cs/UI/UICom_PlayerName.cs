using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_PlayerName : GComponent
{
	public GTextField txt_PlayerName;

	public const string URL = "ui://xuaw6o8jmouys8q";

	public static UICom_PlayerName CreateInstance()
	{
		return (UICom_PlayerName)UIPackage.CreateObject("Common", "Com_PlayerName");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_PlayerName = (GTextField)GetChildAt(0);
	}
}
