using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIExpression_Com_Mark : GComponent
{
	public Controller MapChatColor;

	public Controller showTip;

	public GImage image_Load;

	public UIExpression_Com_Preview com_Preview;

	public const string URL = "ui://mp1ylwytr4n912";

	public static UIExpression_Com_Mark CreateInstance()
	{
		return (UIExpression_Com_Mark)UIPackage.CreateObject("Expression", "Expression_Com_Mark");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		MapChatColor = GetControllerAt(0);
		showTip = GetControllerAt(1);
		image_Load = (GImage)GetChildAt(2);
		com_Preview = (UIExpression_Com_Preview)GetChildAt(3);
	}
}
