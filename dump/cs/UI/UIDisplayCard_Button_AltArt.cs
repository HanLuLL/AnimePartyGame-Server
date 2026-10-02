using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIDisplayCard_Button_AltArt : GComponent
{
	public Controller isSelected;

	public Controller redPoint;

	public GLoader loader_icon;

	public GTextField title;

	public const string URL = "ui://893ze0z8ays2b";

	public static UIDisplayCard_Button_AltArt CreateInstance()
	{
		return (UIDisplayCard_Button_AltArt)UIPackage.CreateObject("DisplayCard", "DisplayCard_Button_AltArt");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		isSelected = GetControllerAt(0);
		redPoint = GetControllerAt(1);
		loader_icon = (GLoader)GetChildAt(0);
		title = (GTextField)GetChildAt(2);
	}
}
