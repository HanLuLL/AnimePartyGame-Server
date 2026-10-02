using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIChat_Button_ExpressionItem : GButton
{
	public GImage image_Selected;

	public UIChat_Com_ExpressionItem com_Expression;

	public GImage image_CollectStatus;

	public GImage image_CollectTip;

	public const string URL = "ui://y0luzhk8ednm13";

	public static UIChat_Button_ExpressionItem CreateInstance()
	{
		return (UIChat_Button_ExpressionItem)UIPackage.CreateObject("Chat", "Chat_Button_ExpressionItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		image_Selected = (GImage)GetChildAt(0);
		com_Expression = (UIChat_Com_ExpressionItem)GetChildAt(1);
		image_CollectStatus = (GImage)GetChildAt(2);
		image_CollectTip = (GImage)GetChildAt(3);
	}
}
