using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGuildDiscovery_Button_SideNavItem : GButton
{
	public Controller redStatus;

	public const string URL = "ui://gldisc01gd002";

	public static UIGuildDiscovery_Button_SideNavItem CreateInstance()
	{
		return (UIGuildDiscovery_Button_SideNavItem)UIPackage.CreateObject("GuildDiscovery", "GuildDiscovery_Button_SideNavItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		redStatus = GetControllerAt(0);
	}
}
