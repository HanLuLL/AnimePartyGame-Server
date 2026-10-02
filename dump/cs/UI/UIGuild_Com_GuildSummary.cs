using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGuild_Com_GuildSummary : GComponent
{
	public Controller canManage;

	public GTextField txt_GuildName;

	public GTextField txt_GuildId;

	public GButton btn_GuildSettings;

	public const string URL = "ui://w5bj58pzlwm19";

	public static UIGuild_Com_GuildSummary CreateInstance()
	{
		return (UIGuild_Com_GuildSummary)UIPackage.CreateObject("Guild", "Guild_Com_GuildSummary");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		canManage = GetControllerAt(0);
		txt_GuildName = (GTextField)GetChildAt(1);
		txt_GuildId = (GTextField)GetChildAt(2);
		btn_GuildSettings = (GButton)GetChildAt(3);
	}
}
