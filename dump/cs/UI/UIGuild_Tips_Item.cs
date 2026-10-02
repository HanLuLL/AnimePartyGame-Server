using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGuild_Tips_Item : GComponent
{
	public GTextField tips;

	public const string URL = "ui://w5bj58pzg04j1i";

	public static UIGuild_Tips_Item CreateInstance()
	{
		return (UIGuild_Tips_Item)UIPackage.CreateObject("Guild", "Guild_Tips_Item");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		tips = (GTextField)GetChildAt(0);
	}
}
