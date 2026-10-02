using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISetting_Com_User : GComponent
{
	public Controller platformState;

	public Controller iosAuditMode;

	public UISetting_Com_InputField txtField_CDK;

	public GButton btn_CDK;

	public UISetting_Com_Button btn_UserAgreement;

	public UISetting_Com_Button btn_Policy;

	public UISetting_Com_Button btn_Developers;

	public UISetting_Com_Button btn_Gratitude;

	public UISetting_Com_Button btn_AccountCenter;

	public UISetting_Com_Button btn_QuitGame;

	public GTextField txt_mail;

	public GGroup txtMail;

	public UISetting_Com_Button btn_ContactService;

	public GButton btn_DeleteAccount;

	public Transition cut_in;

	public const string URL = "ui://iy1joavto1n8z";

	public static UISetting_Com_User CreateInstance()
	{
		return (UISetting_Com_User)UIPackage.CreateObject("Setting", "Setting_Com_User");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		platformState = GetControllerAt(0);
		iosAuditMode = GetControllerAt(1);
		txtField_CDK = (UISetting_Com_InputField)GetChildAt(1);
		btn_CDK = (GButton)GetChildAt(2);
		btn_UserAgreement = (UISetting_Com_Button)GetChildAt(5);
		btn_Policy = (UISetting_Com_Button)GetChildAt(6);
		btn_Developers = (UISetting_Com_Button)GetChildAt(8);
		btn_Gratitude = (UISetting_Com_Button)GetChildAt(9);
		btn_AccountCenter = (UISetting_Com_Button)GetChildAt(11);
		btn_QuitGame = (UISetting_Com_Button)GetChildAt(12);
		txt_mail = (GTextField)GetChildAt(14);
		txtMail = (GGroup)GetChildAt(15);
		btn_ContactService = (UISetting_Com_Button)GetChildAt(17);
		btn_DeleteAccount = (GButton)GetChildAt(18);
		cut_in = GetTransitionAt(0);
	}
}
