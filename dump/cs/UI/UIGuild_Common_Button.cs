using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGuild_Common_Button : GButton
{
	public Controller bgType;

	public const string URL = "ui://w5bj58pzg04jk";

	public static UIGuild_Common_Button CreateInstance()
	{
		return (UIGuild_Common_Button)UIPackage.CreateObject("Guild", "Guild_Common_Button");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		bgType = GetControllerAt(1);
	}
}
