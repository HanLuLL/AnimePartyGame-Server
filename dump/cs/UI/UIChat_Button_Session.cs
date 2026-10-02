using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIChat_Button_Session : GButton
{
	public UIChat_Com_SessionLabel com_Session;

	public Transition Cut_in;

	public const string URL = "ui://y0luzhk8ednmx";

	public static UIChat_Button_Session CreateInstance()
	{
		return (UIChat_Button_Session)UIPackage.CreateObject("Chat", "Chat_Button_Session");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_Session = (UIChat_Com_SessionLabel)GetChildAt(0);
		Cut_in = GetTransitionAt(0);
	}
}
