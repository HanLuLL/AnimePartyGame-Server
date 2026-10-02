using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGuild_Com_ItemMask : GComponent
{
	public GImage zhezhao;

	public const string URL = "ui://w5bj58pzhtd127";

	public static UIGuild_Com_ItemMask CreateInstance()
	{
		return (UIGuild_Com_ItemMask)UIPackage.CreateObject("Guild", "Guild_Com_ItemMask");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		zhezhao = (GImage)GetChildAt(0);
	}
}
