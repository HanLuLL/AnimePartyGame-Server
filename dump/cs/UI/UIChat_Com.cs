using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIChat_Com : GComponent
{
	public GTextField txt_PlayerName;

	public GTextInput Input_Send;

	public GTextField txt_wordLimit;

	public GList list_Session;

	public GList list_Chat;

	public GButton btn_Send;

	public GButton btn_Return;

	public UIChat_Button_OpenExpression btn_Expression;

	public Transition Cut_in;

	public const string URL = "ui://y0luzhk8x1udb";

	public static UIChat_Com CreateInstance()
	{
		return (UIChat_Com)UIPackage.CreateObject("Chat", "Chat_Com");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_PlayerName = (GTextField)GetChildAt(6);
		Input_Send = (GTextInput)GetChildAt(7);
		txt_wordLimit = (GTextField)GetChildAt(8);
		list_Session = (GList)GetChildAt(9);
		list_Chat = (GList)GetChildAt(10);
		btn_Send = (GButton)GetChildAt(11);
		btn_Return = (GButton)GetChildAt(12);
		btn_Expression = (UIChat_Button_OpenExpression)GetChildAt(13);
		Cut_in = GetTransitionAt(0);
	}
}
