using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGuild_Com_ItemBg : GComponent
{
	public Controller SetColor;

	public const string URL = "ui://w5bj58pzhtd125";

	public static UIGuild_Com_ItemBg CreateInstance()
	{
		return (UIGuild_Com_ItemBg)UIPackage.CreateObject("Guild", "Guild_Com_ItemBg");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		SetColor = GetControllerAt(0);
	}
}
