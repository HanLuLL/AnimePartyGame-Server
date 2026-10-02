using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIChat_Button_OpenExpression : GButton
{
	public Controller redPoint;

	public const string URL = "ui://y0luzhk8ednmz";

	public static UIChat_Button_OpenExpression CreateInstance()
	{
		return (UIChat_Button_OpenExpression)UIPackage.CreateObject("Chat", "Chat_Button_OpenExpression");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		redPoint = GetControllerAt(1);
	}
}
