using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIChat_Com_Left : GButton
{
	public Controller state;

	public GTextField txt_Title;

	public GRichTextField txt_Msg;

	public GLoader loader_Head;

	public UIChat_Com_ExpressionItem com_Expression;

	public const string URL = "ui://y0luzhk8b93d3";

	public static UIChat_Com_Left CreateInstance()
	{
		return (UIChat_Com_Left)UIPackage.CreateObject("Chat", "Chat_Com_Left");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		state = GetControllerAt(0);
		txt_Title = (GTextField)GetChildAt(1);
		txt_Msg = (GRichTextField)GetChildAt(2);
		loader_Head = (GLoader)GetChildAt(3);
		com_Expression = (UIChat_Com_ExpressionItem)GetChildAt(4);
	}
}
