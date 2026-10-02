using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIMailPanel : GComponent
{
	public Controller mailType;

	public Controller Status;

	public GButton btn_Return;

	public GGroup bg;

	public GList list_Mail;

	public GButton btn_Delete;

	public GButton btn_All;

	public GGroup mailList;

	public GTextField txt_mailTitle;

	public GTextField txt_MailSender;

	public GTextField txt_MailTime;

	public GLabel com_mailDesc;

	public GGroup mailInfo;

	public GList list_Reward;

	public GButton btn_Reward;

	public GGroup reward;

	public GTextField txt_MailNum;

	public GButton btn_Collect;

	public Transition Cut_in;

	public Transition FJcut_in;

	public const string URL = "ui://91v7tm5dthbm0";

	public static UIMailPanel CreateInstance()
	{
		BindAll();
		return (UIMailPanel)UIPackage.CreateObject("Mail", "MailPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://91v7tm5ds2pwv", typeof(UImail_ScrollBarButton_Grip));
		UIObjectFactory.SetPackageItemExtension("ui://91v7tm5dthbm0", typeof(UIMailPanel));
		UIObjectFactory.SetPackageItemExtension("ui://91v7tm5dthbm1", typeof(UIMail_Button_Label));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		mailType = GetControllerAt(0);
		Status = GetControllerAt(1);
		btn_Return = (GButton)GetChildAt(0);
		bg = (GGroup)GetChildAt(7);
		list_Mail = (GList)GetChildAt(8);
		btn_Delete = (GButton)GetChildAt(9);
		btn_All = (GButton)GetChildAt(10);
		mailList = (GGroup)GetChildAt(11);
		txt_mailTitle = (GTextField)GetChildAt(16);
		txt_MailSender = (GTextField)GetChildAt(18);
		txt_MailTime = (GTextField)GetChildAt(19);
		com_mailDesc = (GLabel)GetChildAt(20);
		mailInfo = (GGroup)GetChildAt(21);
		list_Reward = (GList)GetChildAt(23);
		btn_Reward = (GButton)GetChildAt(25);
		reward = (GGroup)GetChildAt(26);
		txt_MailNum = (GTextField)GetChildAt(29);
		btn_Collect = (GButton)GetChildAt(32);
		Cut_in = GetTransitionAt(0);
		FJcut_in = GetTransitionAt(1);
	}
}
