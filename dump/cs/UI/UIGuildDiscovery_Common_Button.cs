using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGuildDiscovery_Common_Button : GButton
{
	public Controller bgType;

	public const string URL = "ui://gldisc01rc4816";

	public static UIGuildDiscovery_Common_Button CreateInstance()
	{
		return (UIGuildDiscovery_Common_Button)UIPackage.CreateObject("GuildDiscovery", "GuildDiscovery_Common_Button");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		bgType = GetControllerAt(1);
	}
}
