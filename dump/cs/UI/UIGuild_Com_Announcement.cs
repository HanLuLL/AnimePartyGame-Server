using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGuild_Com_Announcement : GComponent
{
	public Controller canEdit;

	public GButton btn_EditAnnouncement;

	public GTextField txt_AnnouncementTitle;

	public GTextField txt_GuildAnnouncement;

	public GTextField txt_AnnouncementUpdatedMeta;

	public const string URL = "ui://w5bj58pzlwm20";

	public static UIGuild_Com_Announcement CreateInstance()
	{
		return (UIGuild_Com_Announcement)UIPackage.CreateObject("Guild", "Guild_Com_Announcement");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		canEdit = GetControllerAt(0);
		btn_EditAnnouncement = (GButton)GetChildAt(2);
		txt_AnnouncementTitle = (GTextField)GetChildAt(3);
		txt_GuildAnnouncement = (GTextField)GetChildAt(4);
		txt_AnnouncementUpdatedMeta = (GTextField)GetChildAt(5);
	}
}
