using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISystemTipsWindow : GComponent
{
	public Controller showMessage;

	public Controller showZoetrope;

	public Controller inviteSignal;

	public Controller chatSignal;

	public GTextField txt_Content;

	public GGroup group_message;

	public UISystemTips_Com_Zeotrope com_Zeotrope;

	public UISystemTips_Button_ChatSignal btn_ChatSignal;

	public UISystemTips_Button_InviteSignal btn_InviteSignal;

	public Transition invite;

	public Transition chat;

	public const string URL = "ui://jrqrz0oqabqa0";

	public static UISystemTipsWindow CreateInstance()
	{
		BindAll();
		return (UISystemTipsWindow)UIPackage.CreateObject("SystemTips", "SystemTipsWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://jrqrz0oq7h6s7", typeof(UISystemTips_Button_InviteSignal));
		UIObjectFactory.SetPackageItemExtension("ui://jrqrz0oqabqa0", typeof(UISystemTipsWindow));
		UIObjectFactory.SetPackageItemExtension("ui://jrqrz0oqljlpd", typeof(UISystemTips_Button_ChatSignal));
		UIObjectFactory.SetPackageItemExtension("ui://jrqrz0oqvlgj2", typeof(UISystemTips_Com_Zeotrope));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		showMessage = GetControllerAt(0);
		showZoetrope = GetControllerAt(1);
		inviteSignal = GetControllerAt(2);
		chatSignal = GetControllerAt(3);
		txt_Content = (GTextField)GetChildAt(1);
		group_message = (GGroup)GetChildAt(2);
		com_Zeotrope = (UISystemTips_Com_Zeotrope)GetChildAt(3);
		btn_ChatSignal = (UISystemTips_Button_ChatSignal)GetChildAt(4);
		btn_InviteSignal = (UISystemTips_Button_InviteSignal)GetChildAt(5);
		invite = GetTransitionAt(0);
		chat = GetTransitionAt(1);
	}
}
