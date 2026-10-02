using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIExpression_Com_Interact_Mobile : GComponent
{
	public Controller style;

	public GButton btn_OpenExpr;

	public GButton btn_OpenChat;

	public UIExpression_Button_MapChat btn_MapChat;

	public GList list_Expression;

	public GList list_Chat;

	public UIExpression_Button_QuickReply btn_QuickReply;

	public Transition showQuickReply;

	public const string URL = "ui://mp1ylwytuapr20";

	public static UIExpression_Com_Interact_Mobile CreateInstance()
	{
		return (UIExpression_Com_Interact_Mobile)UIPackage.CreateObject("Expression", "Expression_Com_Interact_Mobile");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		style = GetControllerAt(0);
		btn_OpenExpr = (GButton)GetChildAt(0);
		btn_OpenChat = (GButton)GetChildAt(1);
		btn_MapChat = (UIExpression_Button_MapChat)GetChildAt(2);
		list_Expression = (GList)GetChildAt(4);
		list_Chat = (GList)GetChildAt(7);
		btn_QuickReply = (UIExpression_Button_QuickReply)GetChildAt(9);
		showQuickReply = GetTransitionAt(0);
	}
}
