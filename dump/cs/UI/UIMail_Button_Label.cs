using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIMail_Button_Label : GButton
{
	public Controller customGray;

	public Controller reward;

	public GTextField txt_mailTitle;

	public GTextField txt_mailSender;

	public GTextField txt_CreateTime;

	public GTextField txt_deadlineTime;

	public GButton com_Item;

	public GButton btn_Collect;

	public Transition cut_in;

	public const string URL = "ui://91v7tm5dthbm1";

	public static UIMail_Button_Label CreateInstance()
	{
		return (UIMail_Button_Label)UIPackage.CreateObject("Mail", "Mail_Button_Label");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		customGray = GetControllerAt(0);
		reward = GetControllerAt(1);
		txt_mailTitle = (GTextField)GetChildAt(1);
		txt_mailSender = (GTextField)GetChildAt(2);
		txt_CreateTime = (GTextField)GetChildAt(3);
		txt_deadlineTime = (GTextField)GetChildAt(4);
		com_Item = (GButton)GetChildAt(6);
		btn_Collect = (GButton)GetChildAt(14);
		cut_in = GetTransitionAt(0);
	}
}
