using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGuildDiscoveryPanel : GComponent
{
	public Controller page;

	public GGraph mohu;

	public UIGuildDiscovery_Com_GuildJoinView com_GuildJoinView;

	public UIGuildDiscovery_Com_ApplicationView com_ApplicationView;

	public UIGuildDiscovery_Com_InviteView com_InviteView;

	public UIGuildDiscovery_Com_CreateView com_CreateView;

	public GButton btn_Back;

	public GList list_GuildSideNavItems;

	public const string URL = "ui://gldisc01gd001";

	public static UIGuildDiscoveryPanel CreateInstance()
	{
		BindAll();
		return (UIGuildDiscoveryPanel)UIPackage.CreateObject("GuildDiscovery", "GuildDiscoveryPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://gldisc01gd001", typeof(UIGuildDiscoveryPanel));
		UIObjectFactory.SetPackageItemExtension("ui://gldisc01gd002", typeof(UIGuildDiscovery_Button_SideNavItem));
		UIObjectFactory.SetPackageItemExtension("ui://gldisc01gd005", typeof(UIGuildDiscovery_Com_GuildListItem));
		UIObjectFactory.SetPackageItemExtension("ui://gldisc01gd007", typeof(UIGuildDiscovery_Com_ApplicationView));
		UIObjectFactory.SetPackageItemExtension("ui://gldisc01gd008", typeof(UIGuildDiscovery_Com_InviteView));
		UIObjectFactory.SetPackageItemExtension("ui://gldisc01gd009", typeof(UIGuildDiscovery_Com_CreateView));
		UIObjectFactory.SetPackageItemExtension("ui://gldisc01gd014", typeof(UIGuildDiscovery_Com_GuildDetai));
		UIObjectFactory.SetPackageItemExtension("ui://gldisc01gd016", typeof(UIGuildDiscovery_Com_GuildJoinView));
		UIObjectFactory.SetPackageItemExtension("ui://gldisc01rc4816", typeof(UIGuildDiscovery_Common_Button));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		page = GetControllerAt(0);
		mohu = (GGraph)GetChildAt(0);
		com_GuildJoinView = (UIGuildDiscovery_Com_GuildJoinView)GetChildAt(1);
		com_ApplicationView = (UIGuildDiscovery_Com_ApplicationView)GetChildAt(2);
		com_InviteView = (UIGuildDiscovery_Com_InviteView)GetChildAt(3);
		com_CreateView = (UIGuildDiscovery_Com_CreateView)GetChildAt(4);
		btn_Back = (GButton)GetChildAt(5);
		list_GuildSideNavItems = (GList)GetChildAt(6);
	}
}
