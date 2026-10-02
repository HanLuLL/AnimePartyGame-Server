using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGuildPanel : GComponent
{
	public Controller page;

	public Controller winType;

	public Controller isInvite;

	public GGraph mohu;

	public UIGuild_Com_GuildHomeView com_GuildHomeView;

	public UIGuild_Com_GuildMenberView com_MemberView;

	public UIGuild_Com_GuildTaskView com_TaskView;

	public UIGuild_Com_GuildStoreView com_StoreView;

	public UIGuild_Setting_win com_tips_win;

	public GButton btn_Back;

	public UIGuild_Com_InviteView com_InviteView;

	public const string URL = "ui://w5bj58pzlwm11";

	public static UIGuildPanel CreateInstance()
	{
		BindAll();
		return (UIGuildPanel)UIPackage.CreateObject("Guild", "GuildPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://w5bj58pzg04j10", typeof(UIGuild_Com_GuildMenberView));
		UIObjectFactory.SetPackageItemExtension("ui://w5bj58pzg04j17", typeof(UIGuild_Member_Item));
		UIObjectFactory.SetPackageItemExtension("ui://w5bj58pzg04j1b", typeof(UIGuild_Setting_win));
		UIObjectFactory.SetPackageItemExtension("ui://w5bj58pzg04j1i", typeof(UIGuild_Tips_Item));
		UIObjectFactory.SetPackageItemExtension("ui://w5bj58pzg04jk", typeof(UIGuild_Common_Button));
		UIObjectFactory.SetPackageItemExtension("ui://w5bj58pzhtd11l", typeof(UIGuild_Com_GuildTaskView));
		UIObjectFactory.SetPackageItemExtension("ui://w5bj58pzhtd11m", typeof(UIGuild_Com_GuildStoreView));
		UIObjectFactory.SetPackageItemExtension("ui://w5bj58pzhtd11p", typeof(UIGuild_Com_TaskLabel));
		UIObjectFactory.SetPackageItemExtension("ui://w5bj58pzhtd125", typeof(UIGuild_Com_ItemBg));
		UIObjectFactory.SetPackageItemExtension("ui://w5bj58pzhtd127", typeof(UIGuild_Com_ItemMask));
		UIObjectFactory.SetPackageItemExtension("ui://w5bj58pzhtd129", typeof(UIGuild_Setting_Com));
		UIObjectFactory.SetPackageItemExtension("ui://w5bj58pzhtd12a", typeof(UIGuild_Notice_Com));
		UIObjectFactory.SetPackageItemExtension("ui://w5bj58pzhtd12b", typeof(UIGuild_MemberSetting_Com));
		UIObjectFactory.SetPackageItemExtension("ui://w5bj58pzhtd12c", typeof(UIGuild_MemberChange_Com));
		UIObjectFactory.SetPackageItemExtension("ui://w5bj58pzhtd12i", typeof(UIGuild_Com_InviteView));
		UIObjectFactory.SetPackageItemExtension("ui://w5bj58pzhtd12j", typeof(UIGuild_Approval_Com));
		UIObjectFactory.SetPackageItemExtension("ui://w5bj58pzhtd12v", typeof(UIGuild_Button_PlayerItem));
		UIObjectFactory.SetPackageItemExtension("ui://w5bj58pzhtd135", typeof(UIGuild_ApprovalItem_Com));
		UIObjectFactory.SetPackageItemExtension("ui://w5bj58pzlwm11", typeof(UIGuildPanel));
		UIObjectFactory.SetPackageItemExtension("ui://w5bj58pzlwm13", typeof(UIGuild_Com_GuildHomeView));
		UIObjectFactory.SetPackageItemExtension("ui://w5bj58pzlwm19", typeof(UIGuild_Com_GuildSummary));
		UIObjectFactory.SetPackageItemExtension("ui://w5bj58pzlwm20", typeof(UIGuild_Com_Announcement));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		page = GetControllerAt(0);
		winType = GetControllerAt(1);
		isInvite = GetControllerAt(2);
		mohu = (GGraph)GetChildAt(0);
		com_GuildHomeView = (UIGuild_Com_GuildHomeView)GetChildAt(1);
		com_MemberView = (UIGuild_Com_GuildMenberView)GetChildAt(2);
		com_TaskView = (UIGuild_Com_GuildTaskView)GetChildAt(3);
		com_StoreView = (UIGuild_Com_GuildStoreView)GetChildAt(4);
		com_tips_win = (UIGuild_Setting_win)GetChildAt(5);
		btn_Back = (GButton)GetChildAt(6);
		com_InviteView = (UIGuild_Com_InviteView)GetChildAt(7);
	}
}
