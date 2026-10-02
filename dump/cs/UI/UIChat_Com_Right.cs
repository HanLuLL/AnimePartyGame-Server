using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIChat_Com_Right : GButton
{
	public Controller state;

	public GLoader loader_Head;

	public GTextField txt_Title;

	public GRichTextField txt_Msg;

	public UIChat_Com_ExpressionItem com_Expression;

	public const string URL = "ui://y0luzhk8b93d6";

	public static UIChat_Com_Right CreateInstance()
	{
		return (UIChat_Com_Right)UIPackage.CreateObject("Chat", "Chat_Com_Right");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		state = GetControllerAt(0);
		loader_Head = (GLoader)GetChildAt(0);
		txt_Title = (GTextField)GetChildAt(2);
		txt_Msg = (GRichTextField)GetChildAt(3);
		com_Expression = (UIChat_Com_ExpressionItem)GetChildAt(4);
	}
}
