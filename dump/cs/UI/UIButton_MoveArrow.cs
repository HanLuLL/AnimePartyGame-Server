using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIButton_MoveArrow : GButton
{
	public GGraph graph_Effect;

	public const string URL = "ui://1ov1i0v9kv7e2d";

	public static UIButton_MoveArrow CreateInstance()
	{
		return (UIButton_MoveArrow)UIPackage.CreateObject("Common_Internal", "Button_MoveArrow");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		graph_Effect = (GGraph)GetChildAt(0);
	}
}
