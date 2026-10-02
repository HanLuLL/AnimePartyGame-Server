using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIChat_Effect : GComponent
{
	public Controller isChoosed;

	public Transition Cut_in;

	public const string URL = "ui://y0luzhk8mfy81f";

	public static UIChat_Effect CreateInstance()
	{
		return (UIChat_Effect)UIPackage.CreateObject("Chat", "Chat_Effect");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		isChoosed = GetControllerAt(0);
		Cut_in = GetTransitionAt(0);
	}
}
