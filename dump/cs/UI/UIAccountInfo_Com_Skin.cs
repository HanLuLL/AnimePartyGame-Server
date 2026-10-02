using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAccountInfo_Com_Skin : GComponent
{
	public GComponent com_Skin;

	public const string URL = "ui://iepldke7f3tl1y";

	public static UIAccountInfo_Com_Skin CreateInstance()
	{
		return (UIAccountInfo_Com_Skin)UIPackage.CreateObject("AccountInfo", "AccountInfo_Com_Skin");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_Skin = (GComponent)GetChildAt(0);
	}
}
