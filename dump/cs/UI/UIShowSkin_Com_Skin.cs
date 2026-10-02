using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIShowSkin_Com_Skin : GComponent
{
	public GComponent com_Skin;

	public const string URL = "ui://zfulrgf7gbr1qqb";

	public static UIShowSkin_Com_Skin CreateInstance()
	{
		return (UIShowSkin_Com_Skin)UIPackage.CreateObject("ShowSkin", "ShowSkin_Com_Skin");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_Skin = (GComponent)GetChildAt(0);
	}
}
