using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIChat_Button_ExpressionTab : GButton
{
	public Controller status;

	public Controller type;

	public UIChat_Com_ExpressionItem com_Expression;

	public UIChat_Button_Pin btn_Pin;

	public GImage image_RedPoint;

	public const string URL = "ui://y0luzhk8ednm15";

	public static UIChat_Button_ExpressionTab CreateInstance()
	{
		return (UIChat_Button_ExpressionTab)UIPackage.CreateObject("Chat", "Chat_Button_ExpressionTab");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(1);
		type = GetControllerAt(2);
		com_Expression = (UIChat_Com_ExpressionItem)GetChildAt(1);
		btn_Pin = (UIChat_Button_Pin)GetChildAt(2);
		image_RedPoint = (GImage)GetChildAt(3);
	}
}
